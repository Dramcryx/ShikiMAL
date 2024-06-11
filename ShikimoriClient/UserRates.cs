namespace ShikimoriClient;

/// <summary>
/// API для запроса userRates в Shikimori
/// </summary>
public class UserRates : GraphQLClientWrapper
{
    public UserRates(Token token)
        : base("https://shikimori.one/api/graphql", token)
    {
    }

    public async Task<List<UserRate>> GetUserRatesAsync()
    {
        int page = 1;
        List<UserRate> result = new();

        // Shikimori отдаёт максимум по 50 записей, поэтому будем листать странички, пока не соберём все нужные данные
        while (true)
        {
            var response = await Common.WaitASec.RunAsync(async () => await GetAsync<UserRatesData>(
                $"{{ userRates(page: {page++}, limit: 50, targetType: Anime) " +
                $"{{ id anime {{ id name russian malId }} episodes status score }} }}"));

            if (response.UserRates is { Count : 0 })
            {
                break;
            }

            result.AddRange(response.UserRates);
        }

        return result;
    }
}
