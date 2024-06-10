using System.Text.Json.Serialization;

namespace MyAnimeListClient;

public class UserAnimeListEdge
{
    [JsonPropertyName("node")] public AnimeShortened Node { get; set; }
    [JsonPropertyName("list_status")] public AnimeListStatus ListStatus { get; set; }
}
