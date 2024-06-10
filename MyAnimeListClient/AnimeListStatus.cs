using System.Text.Json.Serialization;

namespace MyAnimeListClient;

public class AnimeListStatus
{
    [JsonPropertyName("status")] public string Status { get; set; }
    [JsonPropertyName("score")] public int Score { get; set; }
    [JsonPropertyName("num_episodes_watched")] public int NumEpisodesWatched { get; set; }
    [JsonPropertyName("is_rewatching")] public bool IsRewatching { get; set; }
    [JsonPropertyName("start_date")] public string StartDate { get; set; }
    [JsonPropertyName("finish_date")] public string FinishDate { get; set; }
    [JsonPropertyName("priority")] public int Priority { get; set; }
    [JsonPropertyName("num_times_rewatched")] public int NumTimesRewatched { get; set; }
    [JsonPropertyName("tags")] public string[] Tags { get; set; }
    [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; }
}
