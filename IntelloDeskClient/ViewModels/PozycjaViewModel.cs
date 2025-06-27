using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelloDeskClient.DTO;
using IntelloDeskClient.Services;
using IntelloDeskClient.Views;
using System.Collections.ObjectModel;

namespace IntelloDeskClient.ViewModels
{
    public partial class PozycjeViewModel : ObservableObject
    {
        public ObservableCollection<PozycjaDokumentuDto> Pozycje { get; }
        public ObservableCollection<TowarDto> Towary { get; } = new();

        private readonly ApiService _api = new();

        private readonly MainViewModel _main;
        private readonly int _dokumentId;

        public TowarDto? WybranyTowar { get; set; }
        public int NowaIlosc { get; set; }

        public IRelayCommand UsunPozycjeCommand { get; }
        [ObservableProperty]
        private PozycjaDokumentuDto? wybranaPozycja;

        public PozycjeViewModel(List<PozycjaDokumentuDto> pozycje, MainViewModel main, int dokumentId)
        {
            _main = main;
            _dokumentId = dokumentId;
            Pozycje = new ObservableCollection<PozycjaDokumentuDto>(pozycje);

            UsunPozycjeCommand = new RelayCommand<object>(param =>
            {
                if (param is PozycjaDokumentuDto pozycjaDokumentuDto)
                {
                    UsunPozycje(pozycjaDokumentuDto);
                }
            });

            _ = ZaladujTowaryAsync();
        }


        [RelayCommand]
        private void Powrot() => _main.PokazDokumenty();

        [RelayCommand]
        private async Task DodajTowar()
        {
            if (WybranyTowar == null || NowaIlosc < 1)
                return;

            var createPozycjaDokumentuDto = new CreatePozycjaDokumentuDto
            {
                TowarId = WybranyTowar.Id,
                Ilosc = NowaIlosc
            };

            await _api.AddPozycjaAsync(_dokumentId, createPozycjaDokumentuDto);

            Pozycje.Add(new PozycjaDokumentuDto
            {
                TowarId = WybranyTowar.Id,
                NazwaTowaru = WybranyTowar.NazwaTowaru,
                JednostkaMiary = WybranyTowar.JednostkaMiary,
                Ilosc = NowaIlosc
            });

            // Reset formularza
            WybranyTowar = null;
            NowaIlosc = 0;
            OnPropertyChanged(nameof(WybranyTowar));
            OnPropertyChanged(nameof(NowaIlosc));
        }
        private async Task ZaladujTowaryAsync()
        {
            var lista = await _api.GetTowaryAsync();
            Towary.Clear();
            foreach (var towar in lista)
                Towary.Add(towar);
        }

        private async Task UsunPozycje(PozycjaDokumentuDto pozycjaDokumentuDto)
        {
            if (pozycjaDokumentuDto == null) return;

            await _api.DeletePozycjaAsync(_dokumentId, pozycjaDokumentuDto.Id);

            Pozycje.Remove( pozycjaDokumentuDto );
        }
    }


}
