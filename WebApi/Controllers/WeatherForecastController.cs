using Microsoft.AspNetCore.Mvc;
using Shared.Data;
using Shared.Models;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastStorage _weatherForecastStorage = WeatherForecastStorage.Instance;

        [HttpGet("GetForecast")]
        public async Task<ApiResponse> GetWeatherForecast(WeatherForecastRequest request) 
        {
            ApiResponse response = new();

            try
            {
                var weatherForecast = await _weatherForecastStorage.GetWeatherForecastAsync(request.RequestedDateTime);

                response.Success = true;

                response.Result = weatherForecast;
            }
            catch (Exception ex)
            {
                response.Success = false;

                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        [HttpGet("GetForecasts")]
        public ApiResponse GetWeatherForecasts()
        {
            ApiResponse response = new();

            try
            {
                var weatherForecasts = _weatherForecastStorage.GetWeatherForecasts();

                response.Success = true;

                response.Result = weatherForecasts;
            }
            catch (Exception ex)
            {
                response.Success = false;

                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        [HttpGet("GetRequests")]
        public ApiResponse GetRequests() 
        {
            ApiResponse response = new();

            try
            {
                var requests = _weatherForecastStorage.GetRequests();

                response.Success = true;

                response.Result = requests;
            }
            catch (Exception ex) 
            {
                response.Success = false;

                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        [HttpPost("PostForecast")]
        public ApiResponse PostWeatherForecast(WeatherForecast weatherForecast) 
        {
            ApiResponse response = new();

            try
            {
                _weatherForecastStorage.PostWeatherForecast(weatherForecast);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;

                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
