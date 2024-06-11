using System.Text.Json.Serialization;

namespace MyAnimeListClient;

/// <summary>
/// Контракт ноды в ответе на запрос списка аниме
/// </summary>
public class UserAnimeListEdge
{
    [JsonPropertyName("node")]
    public AnimeShortened Node { get; set; }

    [JsonPropertyName("list_status")]
    public AnimeListStatus ListStatus { get; set; }
}
