using System.Text.Json.Serialization;

namespace ShikimoriClient;

public class UserRate
{
    public static string StatusPlanned = "planned";
    public static string StatusWatching = "watching";
    public static string StatusCompleted = "completed";

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("target_id")] public int TargetId { get; set; }
    [JsonPropertyName("target_type")] public string TargetType { get; set; }
    [JsonPropertyName("score")] public int Score { get; set; }
    [JsonPropertyName("status")] public string Status { get; set; }
    [JsonPropertyName("episodes")] public int Episodes { get; set; }
}
