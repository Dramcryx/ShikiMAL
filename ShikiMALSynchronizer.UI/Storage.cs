using System.Text.Json;

namespace ShikiMALSynchronizer.UI;

public class Storage : Common.ICredentialsSaver
{
    public static Storage Instance { get; private set; } = new Storage();

    public async Task SaveCredentialsAsync(string credentialsId, Common.ICredentialsSaver.AppCredentials creds)
    {
        await SecureStorage.SetAsync(credentialsId, JsonSerializer.Serialize(creds));
    }

    public static async Task<Common.ICredentialsSaver.AppCredentials?> GetMyAnimeListCredentialsAsync()
    {
        var resultString = await SecureStorage.GetAsync(Common.ICredentialsSaver.MALCredentialsId);
        if (resultString == null)
            return null;

        return JsonSerializer.Deserialize<Common.ICredentialsSaver.AppCredentials>(resultString);
    }

    public static async Task<Common.ICredentialsSaver.AppCredentials?> GetShikimoriCredentialsAsync()
    {
        var resultString = await SecureStorage.GetAsync(Common.ICredentialsSaver.ShikiCredentialsId);
        if (resultString == null)
            return null;

        return JsonSerializer.Deserialize<Common.ICredentialsSaver.AppCredentials>(resultString);
    }
}
