using System.Collections.Specialized;
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

    public static async Task<Client> AuthorizeAsync(
        Common.IBrowserOpener browserOpener,
        Common.AppIdentifier malAppId,
        Common.ICredentialsSaver? saver = null)
    {
        var result = new Client();

        var codeListenerTask = Common.AuthCodeListener.ListenAuthCodeAsync("http://localhost:5051/oauth/mal/");

        var (challenge, verifier) = Pkce.Generate();

        browserOpener.OpenBrowser(CreateAuthenticationUrl(malAppId.ClientId, challenge));

        result.token = await RequestTokenAsync(malAppId, await codeListenerTask, challenge);

        if (saver != null)
        {
            await saver.SaveCredentialsAsync(
                Common.ICredentialsSaver.MALCredentialsId,
                new Common.ICredentialsSaver.AppCredentials()
                {
                    AccessToken = result.token.AccessToken,
                    RefreshToken = result.token.RefreshToken,
                    ClientId = malAppId.ClientId,
                    ClientSecret = malAppId.ClientSecret
                });
        }

        result.UsersApi = Users.Me(result.token);
        result.AnimeApi = new Anime(result.token);

        return result;
    }

    private static string CreateAuthenticationUrl(string clientId, string challenge)
    {
        NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

        queryString.Add("response_type", "code");
        queryString.Add("client_id", clientId);
        queryString.Add("state", "initial");

        queryString.Add("code_challenge", challenge);
        queryString.Add("code_challenge_method", "plain");

        return $"https://myanimelist.net/v1/oauth2/authorize?{queryString.ToString()}";
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
