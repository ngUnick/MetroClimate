using MetroClimate.Data.Models;
using Microsoft.AspNetCore.Mvc;
using MetroClimate.Services.Services;

namespace MetroClimate.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await _weatherService.GetWeatherForecast();
    }
}
