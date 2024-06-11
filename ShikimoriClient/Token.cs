using System.Text.Json.Serialization;

namespace ShikimoriClient;

/// <summary>
/// Специализация токена для Shikimori
/// </summary>
public class Token : Common.TokenBase
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = "";

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = "";

    [JsonPropertyName("scope")]
    public string Scope { get; set; } = "";

    [JsonPropertyName("created_at")]
    public int CreatedAt { get; set; }
}
