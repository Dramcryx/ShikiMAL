using System.Text.Json.Serialization;

namespace ShikimoriClient;

public class Anime : AnimeShortened
{
    [JsonPropertyName("myanimelist_id")] public int MyAnimeListId { get; set; }
}
