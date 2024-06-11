using System.Collections.Specialized;
using System.Diagnostics;
using System.Text.Json;

namespace MyAnimeListClient;

/// <summary>
/// Основной класс для работы с MyAnimeList. Оборачивает авторизацию и создаёт нужные клиенты к API.
/// </summary>
public class Client
{
    private Token token;

    public Users UsersApi { get; private set; }

    public Anime AnimeApi { get; private set; }

    public static async Task<Client> AuthorizeAsync(Common.AppIdentifier malAppId)
    {
        var result = new Client();

        var codeListenerTask = Common.AuthCodeListener.ListenAuthCodeAsync("http://localhost:5051/oauth/mal/");

        var (challenge, verifier) = Pkce.Generate();

        StartBrowser(malAppId.ClientId, challenge);

        result.token = await RequestTokenAsync(malAppId, await codeListenerTask, challenge);

        result.UsersApi = Users.Me(result.token);
        result.AnimeApi = new Anime(result.token);

        return result;
    }

    private static void StartBrowser(string clientId, string challenge)
    {
        NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

        queryString.Add("response_type", "code");
        queryString.Add("client_id", clientId);
        queryString.Add("state", "initial");

        queryString.Add("code_challenge", challenge);
        queryString.Add("code_challenge_method", "plain");

        var p = new Process();
        p.StartInfo = new ProcessStartInfo($"https://myanimelist.net/v1/oauth2/authorize?{queryString.ToString()}")
        {
            UseShellExecute = true
        };
        p.Start();
    }

    private static async Task<Token> RequestTokenAsync(Common.AppIdentifier appId, string code, string verifier)
    {
        var httpClient = new HttpClient();

        var req = new HttpRequestMessage(HttpMethod.Post, "https://myanimelist.net/v1/oauth2/token");
        req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", appId.ClientId },
                { "grant_type", "authorization_code" },
                { "client_secret", appId.ClientSecret },
                { "code", code },
                { "code_verifier", verifier }
            });

        HttpResponseMessage resp = await httpClient.SendAsync(req);
        var responseString = await resp.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Token>(responseString);
    }
}
