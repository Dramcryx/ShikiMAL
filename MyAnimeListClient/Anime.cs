namespace MyAnimeListClient;

public class Anime : Common.ApiBase<Token>
{
    public Anime(Token token) :
        base("https://api.myanimelist.net/v2/anime", token)
    {
    }

    public async Task PutAnimeAsync(PutAnimeRequest request)
    {
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "status", request.Status },
            { "is_rewatching", request.IsRewatching.ToString().ToLower() },
            { "score", request.Score.ToString() },
            { "num_watched_episodes", request.NumWatchedEpisodes.ToString() }
        });

        await Put($"{request.AnimeId}/my_list_status", content);
    }
}
