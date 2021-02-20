using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using EmployeeAPI.Data;
using EmployeeAPI.Domain;

namespace EmployeeAPI.Controllers
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
        public IActionResult Get()
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            using var db = new DataContext();
            db.Add(new Employee
            {
                FirstName = "Agne",
                LastName = "Mezanskaite",
                BirthDate = new DateTime(1997, 8, 22),
                EmploymentDate = DateTime.UtcNow,
                Boss = null,
                Salary = 5000.0,
                Role = "Gardening"

            });
            db.SaveChanges();
            var user = db.Employees
                .OrderBy(b => b.FirstName)
                .First();
            return Ok(user);
        }
    }
}
