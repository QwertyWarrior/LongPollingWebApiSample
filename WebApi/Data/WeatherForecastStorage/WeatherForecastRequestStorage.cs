using Shared.Models;

namespace WebApi.Data
{
    public class WeatherForecastRequestStorage
    {
        private class WeatherForecastRequestStorageItem
        {
            public DateTime RequestedDateTime { get; set; }

            public TaskCompletionSource<WeatherForecast> Result { get; private set; } = new();
        }

        private readonly List<WeatherForecastRequestStorageItem> _requests = new();

        public TaskCompletionSource<WeatherForecast> Add(DateTime dateTime)
        {
            var storageItem = new WeatherForecastRequestStorageItem()
            {
                RequestedDateTime = dateTime
            };

            _requests.Add(storageItem);

            return storageItem.Result;
        }

        public void SetResponse(WeatherForecast weatherForecast)
        {
            var requests = _requests.Where(rsi => rsi.RequestedDateTime == weatherForecast.DateTime).ToArray();

            foreach (var request in requests)
                if (request.Result.TrySetResult(weatherForecast))
                    _requests.Remove(request);
        }

        public IEnumerable<DateTime> GetRequests() => _requests.Select(rsi => rsi.RequestedDateTime);
    }
}
