using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelloDeskClient.DTO;
using IntelloDeskClient.Services;
using IntelloDeskClient.ViewModels;
using IntelloDeskClient.Views;
using System.Collections.ObjectModel;

public partial class KontrahenciListViewModel : ObservableObject
{
    private readonly ApiService _api = new();
    private readonly MainViewModel _main;

    public KontrahenciListViewModel(MainViewModel main)
    {
        _main = main;
        _ = ZaladujKontrahentowAsync();

        UsunPozycjeCommand = new RelayCommand<object>(param =>
        {
            if (param is KontrahentDto wybranyKontrahentDto)
            {
                UsunPozycje(wybranyKontrahentDto);
            }
        });
    }

    [ObservableProperty]
    private ObservableCollection<KontrahentDto> kontrahenci = new();

    public IRelayCommand UsunPozycjeCommand { get; }
    [ObservableProperty]
    private KontrahentDto? wybranaPozycja;

    [ObservableProperty]
    private KontrahentDto? wybranyKontrahent;

    [RelayCommand]
    private async Task ZaladujKontrahentowAsync()
    {
        var lista = await _api.GetKontrahenciAsync();
        Kontrahenci = new ObservableCollection<KontrahentDto>(lista);
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
    private void CreatekontrahentrView()
    {
        _main.BiezacyWidok = new NowyKontrahentView
        {
            DataContext = new NowyKontrahentViewModel(_main)
        };
    }

    private async Task UsunPozycje(KontrahentDto kontrahentDto)
    {
        if (kontrahentDto == null) return;

        await _api.DeleteKontrahentAsync(kontrahentDto.Id);

        Kontrahenci.Remove(kontrahentDto);
    }
}
