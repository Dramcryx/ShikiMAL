namespace MALShiki;

internal class RatesComparator
{
    internal class ComparisonResult
    {
        public List<ShikimoriRateWithAnime> RatesToAdd { get; set; }

        public List<ShikimoriRateWithAnime> RatesToPatch { get; set; }
    }

    public static ComparisonResult Compare(List<ShikimoriRateWithAnime> shikiRates, List<MyAnimeListClient.UserAnimeListEdge> malRates)
    {
        var malIdToShikiRate = shikiRates.ToDictionary(x => x.Anime.MyAnimeListId);
        var malRatesById = malRates.ToDictionary(x => x.Node.Id);

        return new()
        {
            RatesToAdd = malIdToShikiRate
                .Where(x => !malRatesById.ContainsKey(x.Key))
                .Select(x => x.Value)
                .ToList(),
            RatesToPatch = malIdToShikiRate
                .Where(x => malRatesById.ContainsKey(x.Key))
                .Where(x => x.Value.UserRate.Episodes != malRatesById[x.Key].ListStatus.NumEpisodesWatched)
                .Select(x => x.Value)
                .ToList()
        };
    }
}
