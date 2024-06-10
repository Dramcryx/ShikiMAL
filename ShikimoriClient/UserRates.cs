using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShikimoriClient;

public class UserRates : Common.ApiBase<Token>
{
    class SimplifiedUserModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
    }
    
    private int userId;

    public UserRates(Token token, int userId)
        : base("https://shikimori.one/api/v2/user_rates", token)
    {
        this.userId = userId;
    }

    public static async Task<UserRates> MeAsync(Token token)
    {
        int id = await GetMeAsync(token);
        return new UserRates(token, id);
    }

    public async Task<List<UserRate>> GetAnimeRatesAsync()
    {
        return await Get<List<UserRate>>($"?user_id={userId}");
    }

    private static async Task<int> GetMeAsync(Token token)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, "https://shikimori.one/api/users/whoami");
        req.Headers.Add("User-Agent", "ShikiMAL");
        req.Headers.Add("Authorization", $"Bearer {token.AccessToken}");
        
        var result = await new HttpClient().SendAsync(req);

        var responseAsJson = JsonSerializer.Deserialize<SimplifiedUserModel>(await result.Content.ReadAsStringAsync());
        return responseAsJson.Id;
    }
}
