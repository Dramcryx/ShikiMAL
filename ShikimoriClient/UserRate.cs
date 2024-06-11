namespace ShikimoriClient;

/// <summary>
/// Контракт ноды userRate в ответе на запрос
/// </summary>
public class UserRate
{
    public int Id { get; set; }

    public AnimeShort Anime { get; set; }

    public int Episodes { get; set; }

    public string Status { get; set; }

    public string Score { get; set; }
}
