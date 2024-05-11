using MetroClimate.Data.Database;
using MetroClimate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MetroClimate.Services.Services;

public interface IStationService
{
    Task <List<Station>?> GetUserStationsAsync(int id);
    Task RecordReadingAsync(StationReading reading);
}

public class StationService : IStationService
{
    private MetroClimateDbContext _dbContext;
    
    public StationService(MetroClimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Station>?> GetUserStationsAsync(int userId)
    {
        return await _dbContext.Stations
            .Include(s => s.Sensors)
            .Include(s => s.Readings)
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }
    
    public async Task RecordReadingAsync(StationReading reading)
    {
        _dbContext.StationReadings.Add(reading);
        await _dbContext.SaveChangesAsync();
    }
    
}