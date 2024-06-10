namespace MALShiki;

public class ShikimoriRateWithAnime
{
    public ShikimoriClient.UserRate UserRate { get; set; }

    public ShikimoriClient.Anime Anime { get; set; }

    public static List<ShikimoriRateWithAnime> Zip(List<ShikimoriClient.Anime> animes, List<ShikimoriClient.UserRate> userRates)
    {
        List<ShikimoriRateWithAnime> result = new();

        var userRatesByTargetId = userRates.ToDictionary(x => x.TargetId);
        foreach (var anime in animes)
        {
            result.Add(new() { Anime = anime, UserRate = userRatesByTargetId[anime.Id] });
        }

        return result;
    }
}
