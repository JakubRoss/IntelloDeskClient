using CommunityToolkit.Mvvm.ComponentModel;
using IntelloDeskClient.DTO;
using IntelloDeskClient.Views;

namespace IntelloDeskClient.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] 
        private object? biezacyWidok;

        public MainViewModel()
        {
            PokazStart();
        }

        public void PokazStart()
        {
            BiezacyWidok = new StartView
            {
                DataContext = new StartViewModel(this)
            };
        }

        public void PokazDokumenty()
        {
            BiezacyWidok = new DokumentyListView
            {
                DataContext = new DokumentyListViewModel(this)
            };
        }

    }
}
