namespace ShikiMALSynchronizer.UI;

internal class BrowserOpener : Common.IBrowserOpener
{
    public void OpenBrowser(string url)
    {
        var webView = new WebView();
        App.Current.MainPage = new ContentPage()
        {
            Content = new WebView()
            {
                Source = url
            }
        };
    }
}
