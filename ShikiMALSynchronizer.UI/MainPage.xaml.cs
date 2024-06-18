namespace ShikiMALSynchronizer.UI;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        TryPullCredentialsFromStorageAsync();
    }

    private void OnContinue(object sender, EventArgs e)
    {
        Common.AppIdentifier malId = new()
        {
            ClientId = MAL_CLIENT_ID.Text,
            ClientSecret = MAL_CLIENT_SECRET.Text
        };

        Common.AppIdentifier shikiId = new()
        {
            ClientId = SHIKI_CLIENT_ID.Text,
            ClientSecret = SHIKI_CLIENT_SECRET.Text
        };

        MyAnimeListClient.Client.AuthorizeAsync(new BrowserOpener(), malId, Storage.Instance)
            .ContinueWith(async (malClientTask) => { await ShikimoriAuthAsync(malClientTask.Result, shikiId, Dispatcher); });
    }

    private static async Task ShikimoriAuthAsync(MyAnimeListClient.Client malClient, Common.AppIdentifier shikimoriId, IDispatcher mainThreadDispatcher)
    {
        var shikiClient = await mainThreadDispatcher.DispatchAsync(() => ShikimoriClient.Client.AuthorizeAsync(new BrowserOpener(), shikimoriId, Storage.Instance));
        await mainThreadDispatcher.DispatchAsync(() =>
        {
            var loaderPage = new LoaderPage();
            App.Current.MainPage = loaderPage;
            loaderPage.SetProgressText("Loading animes...");
        });
        var userRates = await shikiClient.UserRatesApi.GetUserRatesAsync();
        await mainThreadDispatcher.DispatchAsync(() => { App.Current.MainPage = new AnimeListPage(userRates); });
    }

    private async void TryPullCredentialsFromStorageAsync()
    {
        var curretnCreds = await Storage.GetMyAnimeListCredentialsAsync();
        if (curretnCreds != null)
        {
            await Dispatcher.DispatchAsync(() =>
            {
                MAL_CLIENT_ID.Text = curretnCreds.ClientId;
                MAL_CLIENT_SECRET.Text = curretnCreds.ClientSecret;
            });
        }

        curretnCreds = await Storage.GetShikimoriCredentialsAsync();
        if (curretnCreds != null)
        {
            await Dispatcher.DispatchAsync(() =>
            {
                SHIKI_CLIENT_ID.Text = curretnCreds.ClientId;
                SHIKI_CLIENT_SECRET.Text = curretnCreds.ClientSecret;
            });
        }
    }
}