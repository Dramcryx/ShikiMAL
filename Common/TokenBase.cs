using System.Text.Json.Serialization;

namespace Common;

/// <summary>
/// Общий токен для шаблона клиента
/// </summary>
public class TokenBase
{
    /// <summary>
    /// Нужный нам токен содержит хотя бы access_token
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = "";
}
