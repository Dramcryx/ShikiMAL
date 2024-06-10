namespace MALShiki;

public static class AnimeExtensions
{
    static readonly Dictionary<string, ShikimoriClient.AnimeStatus> stringToShikimoriStatus = new()
    {
        { "planned", ShikimoriClient.AnimeStatus.Planned },
        { "watching", ShikimoriClient.AnimeStatus.Watching },
        { "rewatching", ShikimoriClient.AnimeStatus.Rewatching },
        { "completed", ShikimoriClient.AnimeStatus.Completed },
        { "on_hold", ShikimoriClient.AnimeStatus.OnHold },
        { "dropped", ShikimoriClient.AnimeStatus.Dropped }
    };

    static readonly Dictionary<string, MyAnimeListClient.AnimeStatus> stringToMALStatus = new()
    {
        { "plan_to_watch", MyAnimeListClient.AnimeStatus.PlanToWatch },
        { "watching", MyAnimeListClient.AnimeStatus.Watching },
        { "completed", MyAnimeListClient.AnimeStatus.Completed },
        { "on_hold", MyAnimeListClient.AnimeStatus.OnHold },
        { "dropped", MyAnimeListClient.AnimeStatus.Dropped }
    };

    static readonly Dictionary<ShikimoriClient.AnimeStatus, string> shikimoriStatusToString = new()
    {
        { ShikimoriClient.AnimeStatus.Planned, "plan_to_watch" },
        { ShikimoriClient.AnimeStatus.Watching, "watching" },
        { ShikimoriClient.AnimeStatus.Rewatching, "watching" },
        { ShikimoriClient.AnimeStatus.Completed, "completed" },
        { ShikimoriClient.AnimeStatus.OnHold, "on_hold" },
        { ShikimoriClient.AnimeStatus.Dropped, "dropped" }
    };

    public static ShikimoriClient.AnimeStatus GetStatus(this ShikimoriClient.UserRate anime)
    {
        return stringToShikimoriStatus[anime.Status];
    }

    public static MyAnimeListClient.AnimeStatus GetStatus(this MyAnimeListClient.UserAnimeListEdge edge)
    {
        return stringToMALStatus[edge.ListStatus.Status];
    }

    public static MyAnimeListClient.PutAnimeRequest ToPatchRequest(this ShikimoriRateWithAnime shiki)
    {
        MyAnimeListClient.PutAnimeRequest result = new();

        result.AnimeId = shiki.Anime.Id;
        result.Status = shiki.UserRate.GetStatus().ToMyAnimeListStatus();
        result.NumWatchedEpisodes = shiki.UserRate.Episodes;
        result.IsRewatching = shiki.UserRate.GetStatus() == ShikimoriClient.AnimeStatus.Rewatching;
        result.Score = shiki.UserRate.Score;

        return result;
    }

    public static string ToMyAnimeListStatus(this ShikimoriClient.AnimeStatus status)
    {
        return shikimoriStatusToString[status];
    }
}
