using Microsoft.EntityFrameworkCore;
using MetroClimate.Data.Database;
using MetroClimate.Data.Models;

namespace MetroClimate.Services.Services;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecast();
}

public class WeatherService : IWeatherService
{
    private MetroClimateDbContext _dbContext;
    
    public WeatherService(MetroClimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecast()
    {
        return await _dbContext.WeatherForecasts.ToListAsync();
    }
    
}