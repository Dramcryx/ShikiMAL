using System.Net;

namespace Common;

/// <summary>
/// Слушатель коллбэка от API при получении Authentication code
/// </summary>
public class AuthCodeListener
{
    /// <summary>
    /// Прослушать по <paramref name="listenerUrl"/> коллбэк от сервера
    /// </summary>
    /// <param name="listenerUrl">Эндпоинт для прослушивания</param>
    /// <returns>Аутентификационный код для получения токена</returns>
    public static Task<string> ListenAuthCodeAsync(string listenerUrl)
    {
        return Task.Run(() => Listen(listenerUrl));
    }

    private static string Listen(string listenerUrl)
    {
        HttpListener _httpListener = new();
        _httpListener.Prefixes.Add(listenerUrl);
        _httpListener.Start();

        var context = _httpListener.GetContext();
        HttpListenerRequest request = context.Request;

        var queryDictionary = System.Web.HttpUtility.ParseQueryString(request.Url.Query);
        var authCode = queryDictionary["code"];

        HttpListenerResponse response = context.Response;

        context.Response.Headers.Clear();
        context.Response.SendChunked = false;
        context.Response.StatusCode = 200;
        context.Response.Close();

        return authCode;
    }
}
