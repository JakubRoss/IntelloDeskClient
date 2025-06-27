using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelloDeskClient.DTO;
using IntelloDeskClient.Services;
using IntelloDeskClient.ViewModels;
using IntelloDeskClient.Views;
using System.Collections.ObjectModel;

public partial class NowePrzyjecieViewModel : ObservableObject
{
    private readonly ApiService _api = new();
    private readonly MainViewModel _main;

    public NowePrzyjecieViewModel(MainViewModel main)
    {
        _main = main;
        _ = ZaladujDaneAsync();
    }

    [ObservableProperty]
    private string symbol = string.Empty;

    [ObservableProperty]
    private int ilosc = 1;

    [ObservableProperty]
    private TowarDto? wybranyTowar;

    [ObservableProperty]
    private KontrahentDto? wybranyKontrahent;

    [ObservableProperty]
    private ObservableCollection<TowarDto> towary = new();

    [ObservableProperty]
    private ObservableCollection<KontrahentDto> kontrahenci = new();

    [RelayCommand]
    private async Task ZaladujDaneAsync()
    {
        var towar = await _api.GetTowaryAsync();
        var kontrahenci = await _api.GetKontrahenciAsync();

        Towary = new ObservableCollection<TowarDto>(towar);
        Kontrahenci = new ObservableCollection<KontrahentDto>(kontrahenci);
    }

    [RelayCommand]
    private async Task Zapisz()
    {
        if (WybranyTowar == null || WybranyKontrahent == null || Ilosc <1)
            return;

        var dto = new CreateDokumentPrzyjeciaDto
        {
            Symbol = Symbol,
            TowarId = WybranyTowar.Id,
            KontrahentId = WybranyKontrahent.Id,
            Ilosc = Ilosc
        };

        await _api.CreateDokumentAsync(dto);
        Anuluj();
    }

    [RelayCommand]
    private void Anuluj()
    {
        _main.BiezacyWidok = new DokumentyListView
        {
            DataContext = new DokumentyListViewModel(_main)
        };
    }
}
