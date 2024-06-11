namespace ShikimoriClient;

/// <summary>
/// Сокращённый контракт anime для запросов в Shikimori
/// </summary>
public class AnimeShort
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Russian { get; set; }

    public int MalId { get; set; }
}