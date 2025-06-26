using System.ComponentModel.DataAnnotations;

namespace IntelloDeskClient.DTO
{
    public class KontrahentDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string Nazwa { get; set; } = string.Empty;

    }
    public class CreateKontrahentDto
    {
        public string Symbol { get; set; } = string.Empty;
        public string Nazwa { get; set; } = string.Empty;
    }
    public class CreateTowarDto
    {
        public string Nazwa { get; set; } = string.Empty;
        public string JednostkaMiary { get; set; } = string.Empty;
    }
    public class TowarDto
    {
        public int Id { get; set; }
        public string NazwaTowaru { get; set; } = string.Empty;
        public string JednostkaMiary { get; set; } = string.Empty ;
    }

    public class PozycjaDokumentuDto
    {
        public int Id { get; set; }
        public int TowarId { get; set; }
        public int DokumentPrzyjeciaId { get; set; }
        public string NazwaTowaru { get; set; } = string.Empty;
        public string JednostkaMiary { get; set; } = string.Empty;
        public decimal Ilosc { get; set; }
    }

    public class DokumentPrzyjeciaDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Symbol { get; set; }
        public KontrahentDto Kontrahent { get; set; }
        public List<PozycjaDokumentuDto> Pozycje { get; set; } = new List<PozycjaDokumentuDto>();
    }

    public class CreateDokumentPrzyjeciaDto
    {

        public string Symbol { get; set; }
        public int KontrahentId { get; set; }
        public int Ilosc { get; set; }
        public int TowarId { get; set; }
    }
    public class CreatePozycjaDokumentuDto
    {
        public int TowarId { get; set; }
        public decimal Ilosc { get; set; }
    }
}
