namespace MyAnimeListClient;

public class User : Common.ApiBase<Token>
{
    string userName;

    public User(Token token, string userName) :
        base("https://api.myanimelist.net/v2/users", token)
    {
        this.userName = userName;
    }

    public static User Me(Token token)
    {
        return new User(token, "@me");
    }

    public async Task<UserGetAnimeListResponse> GetAnimeListAsync()
    {
        return await Get<UserGetAnimeListResponse>($"{userName}/animelist?fields=list_status&limit=1000");
    }
}
