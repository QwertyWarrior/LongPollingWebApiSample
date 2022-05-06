using System.Text.Json.Serialization;

namespace Shared.Models
{
    public class WeatherForecast
    {
        [JsonPropertyName("date_time")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("temperatureC")]
        public int TemperatureC { get; set; }

        [JsonIgnore]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        public override string ToString()
        {
            return $"{nameof(DateTime)}: {DateTime}, {nameof(TemperatureC)}: {TemperatureC}, {nameof(TemperatureF)}: {TemperatureF}, {nameof(Summary)}: {Summary}";
        }
    }
}