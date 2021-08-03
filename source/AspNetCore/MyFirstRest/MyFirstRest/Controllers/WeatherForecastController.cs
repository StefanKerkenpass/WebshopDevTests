using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFirstRest.Model;

namespace MyFirstRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public WeatherForecast[] Get([FromQuery] int? futureDays)
        {
            var rng = new Random();
            return Enumerable.Range(1, futureDays.GetValueOrDefault(5)).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)],
                })
                .ToArray();
        }
        
        [HttpGet("past")]
        public WeatherForecast[] GetA([FromQuery] int? pastDays)
        {
            var rng = new Random();
            return Enumerable.Range(1, pastDays.GetValueOrDefault(5)).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(-index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)],
                })
                .ToArray();
        }
    }
}