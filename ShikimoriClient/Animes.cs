namespace ShikimoriClient;

public class Animes : Common.ApiBase<Token>
{
    public Animes(Token token)
        : base("https://shikimori.one/api/animes", token)
    {
    }

    public async Task<List<Anime>> GetAnimesAsync(IEnumerable<int> ids)
    {
        List<Anime> animes = new List<Anime>();

        foreach (int anime in ids)
        {
            int ticks = Environment.TickCount;
            animes.Add(await GetAnimeAsync(anime));
            
            int ticksToDelay = 1000 - (Environment.TickCount - ticks);
            if (ticksToDelay > 0)
                await Task.Delay(ticksToDelay);

#if false
            var query = string.Join(',', group.Select(x => x.ToString()));
            animes.AddRange(await Get<List<Anime>>($"?ids={query}&limit=50&page{page++}&order=name"));
#endif
        }

        return animes;
    }

    public async Task<Anime> GetAnimeAsync(int id)
    {
        return await Get<Anime>($"/{id}");
    }
}
