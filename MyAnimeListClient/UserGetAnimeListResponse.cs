using System.Text.Json.Serialization;

namespace MyAnimeListClient;

public class UserGetAnimeListResponse
{
    [JsonPropertyName("data")] public List<UserAnimeListEdge> Data { get; set; }

    // paging ignored for now
}

