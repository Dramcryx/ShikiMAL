using System.Text.Json.Serialization;

namespace Common;

public class TokenBase
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; }
}
