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
                .Where(x =>
                    {
                        var malRate = malRatesById[x.Key].ListStatus;
                        return x.Value.Episodes != malRate.NumEpisodesWatched
                            || int.Parse(x.Value.Score) != malRate.Score;
                    })
                .Select(x => x.Value)
                .ToList()
        };
    }
}
