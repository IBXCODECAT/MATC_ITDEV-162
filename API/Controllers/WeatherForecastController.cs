using Microsoft.AspNetCore.Mvc;
using Domain;
using Persistance;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private readonly DataContext _context;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }


    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost]
    public ActionResult<WeatherForecast> Create()
    {
        Console.WriteLine($"Database Path: {_context.dbPath}");
        Console.WriteLine("Insert a new WeatherForecast into the database");

        WeatherForecast forcast = new WeatherForecast
        {
            Date = new DateOnly(),
            TemperatureC = 75,
            Summary = "Warm"
        };

        if(_context.WeatherForecasts == null)
        {
            Console.WriteLine("WeatherForecasts is null");
        }
        else
        {
            _context.WeatherForecasts.Add(forcast);
        }
        
        bool success = _context.SaveChanges() > 0;

        if(success)
        {
            return forcast;
        }

        throw new Exception("Error saving WeatherForecast");
    }
}