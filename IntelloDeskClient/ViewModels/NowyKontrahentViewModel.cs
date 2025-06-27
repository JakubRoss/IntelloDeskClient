using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelloDeskClient.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using IntelloDeskClient.DTO;
    using IntelloDeskClient.Services;
    using IntelloDeskClient.Views;

    public partial class NowyKontrahentViewModel : ObservableObject
    {
        private readonly ApiService _api = new();
        private readonly MainViewModel _main;

        [ObservableProperty]
        private string? symbol;

        [ObservableProperty]
        private string? nazwa;
        public NowyKontrahentViewModel(MainViewModel main)
        {
            _main = main;
        }

        [RelayCommand]
        private async Task AddKontrahent()
        {
            if (string.IsNullOrWhiteSpace(symbol) || string.IsNullOrWhiteSpace(nazwa))
            {
                Anuluj();
                return;
            }
            var dto = new CreateKontrahentDto
            {
                Symbol = Symbol.Trim(),
                Nazwa = Nazwa.Trim()
            };

            await _api.AddKontrahentAsync(dto);
            Anuluj();
        }

        [RelayCommand]
        private void Anuluj()
        {
            _main.BiezacyWidok = new KontrahenciListView
            {
                DataContext = new KontrahenciListViewModel(_main)
            };
        }
    }

}
