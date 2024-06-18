using System.Text.Json.Serialization;

namespace MyAnimeListClient;

/// <summary>
/// Специализация токена для MyAnimeList
/// </summary>
public class Token : Common.TokenBase
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = "";

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}

