using System;

namespace MyFirstRest.Model
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);


        public string Summary { get; set; }

        public Weather Weather => new Weather();

    }

    public class Weather
    {
        private static readonly string[] Weathers = new[]
        {
            "Raining", "Sunny", "Cloudy", "Snowing", "Stormy", "Hail", "Thunderstorm"
        };

        public string Name => GetRandomWeather();

        private string GetRandomWeather()
        {
            var rng = new Random();
            return Weathers[rng.Next(Weathers.Length)];
        }
    }
}