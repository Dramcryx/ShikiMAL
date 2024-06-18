using ShikiMALSynchronizer.UI.VievModel;

namespace ShikiMALSynchronizer.UI;

public partial class AnimeListPage : ContentPage
{
	public AnimeListPage(List<ShikimoriClient.UserRate> userRates)
	{
		InitializeComponent();
		var bindingContext = new UserRatesViewModel();
		foreach (var userRate in userRates)
		{
			bindingContext.UserRates.Add(userRate);
		}
		BindingContext = bindingContext;
	}
}