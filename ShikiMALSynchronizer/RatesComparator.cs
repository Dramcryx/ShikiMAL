namespace MALShiki;

internal class RatesComparator
{
    internal class ComparisonResult
    {
        public List<ShikimoriClient.UserRate> RatesToAdd { get; set; }

        public List<ShikimoriClient.UserRate> RatesToPatch { get; set; }
    }

    public static ComparisonResult Compare(List<ShikimoriClient.UserRate> shikiRates, List<MyAnimeListClient.UserAnimeListEdge> malRates)
    {
        var malIdToShikiRate = shikiRates.ToDictionary(x => x.Anime.MalId);
        var malRatesById = malRates.ToDictionary(x => x.Node.Id);

        return new()
        {
            RatesToAdd = malIdToShikiRate
                .Where(x => !malRatesById.ContainsKey(x.Key))
                .Select(x => x.Value)
                .ToList(),
            RatesToPatch = malIdToShikiRate
                .Where(x => malRatesById.ContainsKey(x.Key))
                .Where(x => x.Value.Episodes != malRatesById[x.Key].ListStatus.NumEpisodesWatched)
                .Select(x => x.Value)
                .ToList()
        };
    }
}
