using System.Text.Json.Serialization;

namespace CatalogApi.DTO
{
    public record DateTimeDTO
    {
        public DateTime DateTime { get; init; }
        public string? Date { get; init; }
        public string? Time { get; init; }
        public string? Day { get; init; }

        [JsonConstructor]
        public DateTimeDTO(DateTime dat)
        {
            DateTime = dat;
            Date = DateTime.ToString("dd.MM.yyyy");
            Time = DateTime.ToString("HH:mm:ss");
            Day = DateTime.ToString("dddd");
        }

        public static async Task<DateTimeDTO> GetCurrent()
        {
            return new DateTimeDTO(DateTime.Now);
        }
    }
}