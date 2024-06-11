namespace Common;

/// <summary>
/// Общий для обоих API тип идентификатора клиентского приложения
/// </summary>
public class AppIdentifier
{
    /// <summary>
    /// Поле client_id для запросов в API
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// Поле client_secret для запросов в API
    /// </summary>
    public string ClientSecret { get; set; }
}
