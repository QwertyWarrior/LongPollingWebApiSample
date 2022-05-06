using Shared.Models;

namespace WebApiClient
{
    class Programm 
    {
        static void Main(string[] args)
        {
            Random rng = new();

            string[] summaries = new[] 
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            ApiClient api = new() 
            {
                ApiHelper = new()
                {
                    BaseAddress = "http://localhost:5089",
                    ControllerAddress = "api/WeatherForecast"
                }
            };

            while (true) 
            {
                Console.WriteLine("1 - GetWeatherForecasts");
                Console.WriteLine("2 - GetWeatherForecast");
                Console.WriteLine("3 - GetRequests");
                Console.WriteLine("4 - PostWeatherForecast");

                int selectedIndex = Console.ReadKey().KeyChar - 48;

                Console.WriteLine();

                switch (selectedIndex) 
                {
                    case 1:

                        api.GetWeatherForecastsAsync().ContinueWith(weatherForecasts => 
                        {
                            if (weatherForecasts.Result?.Count() > 0)
                                Console.WriteLine($"{nameof(weatherForecasts)}:\n[\n\t{string.Join(",\n\t", weatherForecasts.Result)}\n]");
                            else
                                Console.WriteLine($"{nameof(weatherForecasts)}: []");
                        });

                        break;

                    case 2:
                        {
                            Console.Write("dateTime: ");

                            if (DateTime.TryParse(Console.ReadLine(), out DateTime dateTime))
                            {
                                api.GetWeatherForecastAsync(dateTime).ContinueWith(weatherForecast => 
                                {
                                    Console.WriteLine($"{nameof(weatherForecast)}:\n{weatherForecast.Result}");
                                });
                            }
                        }

                        break;

                    case 3:

                        api.GetRequestsAsync().ContinueWith(requests => 
                        {
                            if (requests.Result?.Count() > 0)
                                Console.WriteLine($"{nameof(requests)}:\n[\n\t{string.Join(",\n\t", requests.Result)}\n]");
                            else
                                Console.WriteLine($"{nameof(requests)}: []");
                        });

                        break;

                    case 4:
                        {
                            Console.Write("dateTime: ");

                            if (DateTime.TryParse(Console.ReadLine(), out DateTime dateTime))
                            {
                                WeatherForecast weatherForecast = new()
                                {
                                    DateTime = dateTime,
                                    TemperatureC = rng.Next(0, 100),
                                    Summary = summaries[rng.Next(summaries.Length)]
                                };

                                api.PostForecastAsync(weatherForecast);
                            }
                        }
                        break;
                }
            }
        }
    }
}