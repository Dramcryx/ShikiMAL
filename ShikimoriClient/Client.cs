﻿using System.Collections.Specialized;
using System.Text.Json;

namespace ShikimoriClient;

/// <summary>
/// Основной класс для работы с Shikimori. Оборачивает авторизацию и создаёт нужные клиенты к API.
/// </summary>
public class Client
{
    private Token token;

    public UserRates UserRatesApi { get; private set; }

    public static async Task<Client> AuthorizeAsync(
        Common.IBrowserOpener browserOpener,
        Common.AppIdentifier shikiAppId,
        Common.ICredentialsSaver? saver = null)
    {
        var result = new Client();

        var codeListenerTask = Common.AuthCodeListener.ListenAuthCodeAsync("http://localhost:5051/oauth/shikimori/");

        browserOpener.OpenBrowser(CreateAuthenticationUrl(shikiAppId.ClientId));

        result.token = await RequestTokenAsync(shikiAppId, await codeListenerTask);

        if (saver != null)
        {
            await saver.SaveCredentialsAsync(
                Common.ICredentialsSaver.ShikiCredentialsId,
                new Common.ICredentialsSaver.AppCredentials()
                {
                    AccessToken = result.token.AccessToken,
                    RefreshToken = result.token.RefreshToken,
                    ClientId = shikiAppId.ClientId,
                    ClientSecret = shikiAppId.ClientSecret
                });
        }

        result.UserRatesApi = new UserRates(result.token);

        return result;
    }

    private static string CreateAuthenticationUrl(string clientId)
    {
        NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

        queryString.Add("client_id", clientId);
        queryString.Add("redirect_uri", "http://localhost:5051/oauth/shikimori/");
        queryString.Add("response_type", "code");
        queryString.Add("scope", "user_rates");

        return $"https://shikimori.one/oauth/authorize?{queryString.ToString()}";
    }

    private static async Task<Token> RequestTokenAsync(Common.AppIdentifier appId, string code)
    {
        var httpClient = new HttpClient();

        var req = new HttpRequestMessage(HttpMethod.Post, "https://shikimori.one/oauth/token");
        req.Headers.Add("User-Agent", "ShikiMAL");
        // This is the important part:
        req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "client_id", appId.ClientId },
                { "client_secret", appId.ClientSecret },
                { "code", code },
                { "redirect_uri", "http://localhost:5051/oauth/shikimori/" }
            });

        HttpResponseMessage resp = await httpClient.SendAsync(req);

        var responseString = await resp.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Token>(responseString);
    }
}
