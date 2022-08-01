using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitizensController : ControllerBase
    {
        // private static readonly string[] Summaries = new[]
        // {
        //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        // };

        private readonly ILogger<CitizensController> _logger;

        public CitizensController(ILogger<CitizensController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetCitizens")]
        public Citizens[] Get()
        {
            // return Enumerable.Range(1, 5)
            //     .Select(index => new WeatherForecast
            //     {
            //         Date = DateTime.Now.AddDays(index),
            //         TemperatureC = Random.Shared.Next(-20, 55),
            //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //     })
            //     .ToArray();
            return new []
                {
                    new Citizens{id = 1, first_name = "jfhg"},
                    new Citizens{id = 2, first_name = "fjhfgf"},
                    new Citizens{id = 3, first_name = "fjhghf"}
                };
        }
    }
}