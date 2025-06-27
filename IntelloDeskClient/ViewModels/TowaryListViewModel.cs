using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelloDeskClient.DTO;
using IntelloDeskClient.Services;
using IntelloDeskClient.Views;
using System.Collections.ObjectModel;

namespace IntelloDeskClient.ViewModels
{
    public partial class TowaryListViewModel : ObservableObject
    {
        private readonly ApiService _api = new();
        private readonly MainViewModel _main;

        public TowaryListViewModel(MainViewModel main)
        {
            _main = main;
            _ = ZaladujTowaryAsync();

            UsunPozycjeCommand = new RelayCommand<object>(param =>
            {
                if (param is TowarDto wybranyKontrahentDto)
                {
                    UsunPozycje(wybranyKontrahentDto);
                }
            });
        }

        [ObservableProperty]
        private ObservableCollection<TowarDto> towar = new();

        public IRelayCommand UsunPozycjeCommand { get; }
        [ObservableProperty]
        private KontrahentDto? wybranaPozycja;

        [ObservableProperty]
        private KontrahentDto? wybranyTowar;

        [RelayCommand]
        private async Task ZaladujTowaryAsync()
        {
            var lista = await _api.GetTowaryAsync();
            Towar = new ObservableCollection<TowarDto>(lista);
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
        private void CreateTowarView()
        {
            _main.BiezacyWidok = new NowyTowarView
            {
                DataContext = new NowyTowarViewModel(_main)
            };
        }

        private async Task UsunPozycje(TowarDto towarDto)
        {
            if (towarDto == null) return;

            await _api.DeleteTowarAsync(towarDto.Id);

            Towar.Remove(towarDto);
        }
    }
}
