using Shared.Models;

namespace WebApi.Data
{
    public class WeatherForecastStorage
    {
        public static WeatherForecastStorage Instance { get; private set; } = new();

        private readonly Dictionary<DateTime, WeatherForecast>  _weatherForecasts = new();
        private readonly WeatherForecastRequestStorage _requestStorage = new();

        public IEnumerable<WeatherForecast> GetWeatherForecasts() 
        {
            return _weatherForecasts.Select(kvp => kvp.Value);
        }

        public Task<WeatherForecast> GetWeatherForecastAsync(DateTime dateTime) 
        {
            var source = _requestStorage.Add(dateTime);

            if (_weatherForecasts.ContainsKey(dateTime))
                _requestStorage.SetResponse(_weatherForecasts[dateTime]);
            
            return source.Task;
        }

        public void PostWeatherForecast(WeatherForecast weatherForecast) 
        {
            if (_weatherForecasts.ContainsKey(weatherForecast.DateTime))
            {
                _weatherForecasts[weatherForecast.DateTime] = weatherForecast;
            }
            else 
            {
                _weatherForecasts.Add(weatherForecast.DateTime, weatherForecast);
            }

            _requestStorage.SetResponse(weatherForecast);
        }

        public IEnumerable<DateTime> GetRequests() 
        {
            return _requestStorage.GetRequests();
        }
    }
}
