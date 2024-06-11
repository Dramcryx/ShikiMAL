using System.Text.Json.Serialization;

namespace MyAnimeListClient;

/// <summary>
/// Контракт ответа на запрос списка аниме пользователя
/// </summary>
public class UserGetAnimeListResponse
{
    [JsonPropertyName("data")]
    public List<UserAnimeListEdge> Data { get; set; }

    // paging ignored for now
}

