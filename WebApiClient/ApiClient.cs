using Shared.Data;
using Shared.Models;
using Shared.Wrappers;

namespace WebApiClient
{
    public class ApiClient
    {
        private ApiHelper? _apiHelper;

        public ApiHelper ApiHelper 
        {
            get 
            {
                if (_apiHelper == null)
                    _apiHelper = ApiHelper.Instance;

                return _apiHelper;
            }

            set 
            {
                _apiHelper = value;
            }
        }

        public Task<IEnumerable<WeatherForecast>?> GetWeatherForecastsAsync(CancellationToken? cancellationToken = null) 
        {
            return ApiHelper.CallGetMethodAsync<IEnumerable<WeatherForecast>>("GetForecasts", cancellationToken: cancellationToken);
        }

        public Task<WeatherForecast?> GetWeatherForecastAsync(DateTime dateTime, CancellationToken? cancellationToken = null) 
        {
            WeatherForecastRequest request = new()
            {
                RequestedDateTime = dateTime
            };

            return ApiHelper.CallGetMethodAsync<WeatherForecast>("GetForecast", content: request, cancellationToken: cancellationToken);
        }

        public Task<IEnumerable<DateTime>?> GetRequestsAsync(CancellationToken? cancellationToken = null) 
        {
            return ApiHelper.CallGetMethodAsync<IEnumerable<DateTime>>("GetRequests", cancellationToken: cancellationToken);
        }

        public Task<ApiResponse?> PostForecastAsync(WeatherForecast weatherForecast, CancellationToken? cancellationToken = null) 
        {
            return ApiHelper.CallPostMethodAsync("PostForecast", content: weatherForecast, cancellationToken: cancellationToken);
        }
    }
}
