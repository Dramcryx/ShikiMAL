using MALShiki;

Console.WriteLine("Hello, World!");

var MALClient = await MyAnimeListClient.Client.AuthorizeAsync(
    new Common.AppIdentifier()
    {
        ClientId = Environment.GetEnvironmentVariable("MAL_CLIENT_ID"),
        ClientSecret = Environment.GetEnvironmentVariable("MAL_CLIENT_SECRET")
    });

var MALRates = (await MALClient.UsersApi.GetAnimeListAsync()).Data;

var ShikiClient = await ShikimoriClient.Client.AuthorizeAsync(
    new Common.AppIdentifier()
    {
        ClientId = Environment.GetEnvironmentVariable("SHIKI_CLIENT_ID"),
        ClientSecret = Environment.GetEnvironmentVariable("SHIKI_CLIENT_SECRET")
    });

var ShikiRates = await ShikiClient.UserRatesApi.GetUserRatesAsync();

var comparison = RatesComparator.Compare(ShikiRates, MALRates);

Console.WriteLine("Animes to add in MAL:\n" + string.Join('\n', comparison.RatesToAdd.Select(x => x.Anime.Name)));
Console.WriteLine("Animes to patch in MAL:\n" + string.Join('\n', comparison.RatesToPatch.Select(x => x.Anime.Name)));

foreach (var toAdd in comparison.RatesToAdd)
{
    await Common.WaitASec.RunAsync(async () => await MALClient.AnimeApi.PutAnimeAsync(toAdd.ToPatchRequest()), 1500);
}

foreach (var toPatch in comparison.RatesToPatch)
{
    await Common.WaitASec.RunAsync(async () => await MALClient.AnimeApi.PutAnimeAsync(toPatch.ToPatchRequest()), 1500);
}

Console.WriteLine("Done");
Console.ReadLine();
