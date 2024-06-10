using System.Text.Json;

namespace Common;

public class ApiBase<TokenT> where TokenT : TokenBase
{
    private HttpClient httpClient = new();

    private string endpointBase;

    private TokenT token;

    public ApiBase(string endpointBase, TokenT token)
    {
        this.endpointBase = endpointBase;
        this.token = token;
    }

    protected async Task<T> Get<T>(string url)
    {
        var response = await httpClient.SendAsync(CreateRequest(HttpMethod.Get, url));
        var responseAsString = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<T>(responseAsString);
    }

    protected async Task Put(string url, FormUrlEncodedContent content)
    {
        var request = CreateRequest(HttpMethod.Put, url);
        request.Content = content;
        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string url)
    {
        var req = new HttpRequestMessage(method, $"{endpointBase}/{url}");
        req.Headers.Add("Authorization", $"Bearer {token.AccessToken}");
        return req;
    }
}
