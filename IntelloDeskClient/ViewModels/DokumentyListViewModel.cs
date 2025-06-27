using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelloDeskClient.DTO;
using IntelloDeskClient.Services;
using IntelloDeskClient.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace IntelloDeskClient.ViewModels
{
    public partial class DokumentyListViewModel : ObservableObject
    {
        private readonly MainViewModel _main;
        private readonly ApiService _api = new();
        public IRelayCommand PokazPozycjeCommand { get; }
        public IRelayCommand UsunDokumentCommand { get; }

        public DokumentyListViewModel(MainViewModel main)
        {
            _main = main;
            _ = ZaladujAsync();

            //zbadaj sprawe
            PokazPozycjeCommand = new RelayCommand<object>(param =>
            {
                if (param is DokumentPrzyjeciaDto dokument)
                {
                    PokazPozycje(dokument);
                }
            });

            UsunDokumentCommand = new RelayCommand<object>(param =>
            {
                if (param is DokumentPrzyjeciaDto dokument)
                {
                    UsunDokument(dokument.Id);
                }
            });

        }

        [ObservableProperty]
        private ObservableCollection<DokumentPrzyjeciaDto> dokumenty = new();

        [ObservableProperty]
        private DokumentPrzyjeciaDto? selectedDokument;

        [RelayCommand]
        private async Task ZaladujAsync()
        {
            var lista = await _api.GetDokumentyAsync();
            Dokumenty = new ObservableCollection<DokumentPrzyjeciaDto>(lista);
        }

        [RelayCommand]
        private void Powrot()
        {
            _main.BiezacyWidok = new StartView
            {
                DataContext = new StartViewModel(_main)
            };
        }

        [RelayCommand]
        private void CreateDokumentView()
        {
            _main.BiezacyWidok = new NowePrzyjecieView
            {
                DataContext = new NowePrzyjecieViewModel(_main)
            };
        }

        private void PokazPozycje(DokumentPrzyjeciaDto dokument)
        {
            _main.BiezacyWidok = new PozycjaView
            {
                DataContext = new PozycjeViewModel(dokument.Pozycje, _main, dokument.Id)
            };
        }
        private async Task UsunDokument(int id)
        {
            await _api.DeleteDokumentAsync(id);
            await ZaladujAsync();
        }
    }
}
