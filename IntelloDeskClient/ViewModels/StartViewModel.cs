using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelloDeskClient.Views;

namespace IntelloDeskClient.ViewModels
{
    public partial class StartViewModel : ObservableObject
    {
        private readonly MainViewModel _main;

        public StartViewModel(MainViewModel main)
        {
            _main = main;
        }

        [RelayCommand]
        private void PokazDokumenty()
        {
            _main.BiezacyWidok = new DokumentyListView
            {
                DataContext = new DokumentyListViewModel(_main)
            };
        }

        [RelayCommand]
        private void PokazKontrahenci()
        {
            _main.BiezacyWidok = new KontrahenciListView
            {
                DataContext = new KontrahenciListViewModel(_main)
            };
        }

        [RelayCommand]
        private void PokazTowary()
        {
            _main.BiezacyWidok = new TowaryListView
            {
                DataContext = new TowaryListViewModel(_main)
            };
        }
    }
}
