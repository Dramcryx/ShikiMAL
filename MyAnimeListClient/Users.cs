namespace MyAnimeListClient;

/// <summary>
/// API для эндпоинта /v2/users на MyAnimeList
/// </summary>
public class Users : Common.ApiBase<Token>
{
    private readonly string userName;

    public Users(Token token, string userName) :
        base("https://api.myanimelist.net/v2/users", token)
    {
        this.userName = userName;
    }

    public static Users Me(Token token)
    {
        return new Users(token, "@me");
    }

    public async Task<UserGetAnimeListResponse> GetAnimeListAsync()
    {
        return await Get<UserGetAnimeListResponse>($"{userName}/animelist?fields=list_status&limit=1000");
    }
}
