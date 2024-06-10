using System.Net;

namespace Common;

public abstract class AuthCodeListener
{
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

        // Obtain a response object.
        HttpListenerResponse response = context.Response;

        context.Response.Headers.Clear();
        context.Response.SendChunked = false;
        context.Response.StatusCode = 200;
        context.Response.Close();

        return authCode;
    }
}
