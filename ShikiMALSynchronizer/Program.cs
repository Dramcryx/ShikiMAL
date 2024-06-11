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
    int ticks = Environment.TickCount;
    await MALClient.AnimeApi.PutAnimeAsync(toAdd.ToPatchRequest());
    int ticksToDelay = 1000 - (Environment.TickCount - ticks);
    if (ticksToDelay > 0)
        await Task.Delay(ticksToDelay);
}

foreach (var toPatch in comparison.RatesToPatch)
{
    int ticks = Environment.TickCount;
    await MALClient.AnimeApi.PutAnimeAsync(toPatch.ToPatchRequest());
    int ticksToDelay = 1000 - (Environment.TickCount - ticks);
    if (ticksToDelay > 0)
        await Task.Delay(ticksToDelay);
}

Console.WriteLine("Done");
Console.ReadLine();
