namespace Common;

public interface ICredentialsSaver
{
    public static string MALCredentialsId = "MAL";

    public static string ShikiCredentialsId = "Shiki";

    public class AppCredentials
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;
    }

    public Task SaveCredentialsAsync(string credentialsId, AppCredentials creds);
}
