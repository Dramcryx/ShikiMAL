using System.Text.Json.Serialization;

namespace MyAnimeListClient;

public class PutAnimeRequest
{
    public int AnimeId { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("is_rewatching")]
    public bool IsRewatching { get; set; }
    
    [JsonPropertyName("score")]
    public int Score { get; set; }
    
    [JsonPropertyName("num_watched_episodes")]
    public int NumWatchedEpisodes { get; set; }

    // Свойства ниже пока не требуются
    
    //[JsonPropertyName("priority")] public int Priority { get; set; }
    
    //[JsonPropertyName("num_times_rewatched")] public int NumTimesRewatched { get; set; }
    
    //[JsonPropertyName("rewatch_value")] public int RewatchValue { get; set; }
    
    //[JsonPropertyName("tags")] public List<string> Tags { get; set; }
    
    //[JsonPropertyName("comments")] public List<string> Comments { get; set; }
}
