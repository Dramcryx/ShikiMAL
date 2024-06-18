using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShikiMALSynchronizer.UI.VievModel;

class UserRatesViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ShikimoriClient.UserRate> UserRates { get; private set; } = new();

    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
