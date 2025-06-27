namespace IntelloDeskClient.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using IntelloDeskClient.DTO;
    using IntelloDeskClient.Services;
    using IntelloDeskClient.Views;

    public partial class NowyTowarViewModel : ObservableObject
    {
        private readonly ApiService _api = new();
        private readonly MainViewModel _main;

        public NowyTowarViewModel(MainViewModel main)
        {
            _main = main;
        }

        [ObservableProperty]
        private string nazwa = string.Empty;

        [ObservableProperty]
        private string jednostkaMiary = string.Empty;

        [RelayCommand]
        private async Task Zapisz()
        {
            if (string.IsNullOrWhiteSpace(Nazwa) || string.IsNullOrWhiteSpace(JednostkaMiary))
                return;

            var dto = new CreateTowarDto
            {
                Nazwa = Nazwa.Trim(),
                JednostkaMiary = JednostkaMiary.Trim()
            };

            await _api.AddTowarAsync(dto);
            Anuluj();
        }

        [RelayCommand]
        private void Anuluj()
        {
            _main.BiezacyWidok = new StartView
            {
                DataContext = new StartViewModel(_main)
            };
        }
    }

}
