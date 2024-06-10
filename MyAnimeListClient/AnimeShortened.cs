using System.Text.Json.Serialization;

namespace MyAnimeListClient;

public class AnimeShortened
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
}
