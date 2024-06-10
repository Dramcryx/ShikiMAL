using System.Text.Json.Serialization;

namespace ShikimoriClient;

public class AnimeShortened
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("russian")] public string Russian { get; set; }
    // image field ignored
    [JsonPropertyName("url")] public string Url { get; set; }
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("score")] public string Score { get; set; }
    [JsonPropertyName("episodes")] public int Episodes { get; set; }
}
