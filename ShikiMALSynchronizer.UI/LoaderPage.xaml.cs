namespace ShikiMALSynchronizer.UI;

public partial class LoaderPage : ContentPage
{
	public LoaderPage()
	{
		InitializeComponent();
	}

	public void SetProgressText(string text)
	{
		progressLabel.Text = text;
	}
}