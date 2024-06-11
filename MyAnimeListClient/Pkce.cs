using System.Security.Cryptography;
using System.Text;

namespace MyAnimeListClient;

/// <summary>
/// Генератор PKCE для авторизации в MyAniemList
/// </summary>
static class Pkce
{
    /// <summary>
    /// Генерирует code_verifier и соответствующий code_challenge согласно RFC-7636.
    /// </summary>
    /// <remarks>См. https://datatracker.ietf.org/doc/html/rfc7636#section-4.1 и https://datatracker.ietf.org/doc/html/rfc7636#section-4.2</remarks>
    public static (string code_challenge, string verifier) Generate(int size = 32)
    {
        using var rng = RandomNumberGenerator.Create();
        var randomBytes = new byte[size];
        rng.GetBytes(randomBytes);
        var verifier = Base64UrlEncode(randomBytes);

        var buffer = Encoding.UTF8.GetBytes(verifier);
        var hash = SHA256.Create().ComputeHash(buffer);
        var challenge = Base64UrlEncode(hash);

        return (challenge, verifier);
    }

    private static string Base64UrlEncode(byte[] data) =>
        Convert.ToBase64String(data)
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');
}
