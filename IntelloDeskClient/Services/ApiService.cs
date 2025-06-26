using IntelloDeskClient.DTO;
using System.Net.Http;
using System.Net.Http.Json;

namespace IntelloDeskClient.Services
{
    class ApiService
    {
        private readonly HttpClient _http;

        public ApiService()
        {
            _http = new HttpClient();
            _http.BaseAddress = new Uri("https://qubity.azurewebsites.net/api/");
        }


        // Dokumenty
        public async Task<List<DokumentPrzyjeciaDto>?> GetDokumentyAsync() =>
            await _http.GetFromJsonAsync<List<DokumentPrzyjeciaDto>>("dokumenty");

        public async Task CreateDokumentAsync(CreateDokumentPrzyjeciaDto dto)
        {
            var res = await _http.PostAsJsonAsync("dokumenty", dto);
            res.EnsureSuccessStatusCode();
        }

        public async Task DeleteDokumentAsync(int id) =>
            await _http.DeleteAsync($"dokumenty/{id}");

        // Kontrahenci
        public async Task<List<KontrahentDto>> GetKontrahenciAsync() =>
            await _http.GetFromJsonAsync<List<KontrahentDto>>("kontrahent");

        public async Task AddKontrahentAsync(CreateKontrahentDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto), "DTO nie może być null.");

            var response = await _http.PostAsJsonAsync("kontrahent", dto);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteKontrahentAsync(int id) =>
            await _http.DeleteAsync($"kontrahent/{id}");

        // Towary (do wyboru w Pozycjach)
        public async Task<List<TowarDto>> GetTowaryAsync() =>
            await _http.GetFromJsonAsync<List<TowarDto>>("towary");

        public async Task AddTowarAsync(CreateTowarDto dto) =>
            await _http.PostAsJsonAsync("towary",dto);

        public async Task DeleteTowarAsync(int id) =>
            await _http.DeleteAsync($"towary/{id}");

        // Pozycje dokumentu
        public async Task AddPozycjaAsync(int dokumentId, CreatePozycjaDokumentuDto dto) =>
            await _http.PostAsJsonAsync($"dokumenty/{dokumentId}/pozycje", dto);

        public async Task DeletePozycjaAsync(int dokumentId, int pozycjaId) =>
            await _http.DeleteAsync($"dokumenty/{dokumentId}/pozycje/{pozycjaId}");
    }
}
