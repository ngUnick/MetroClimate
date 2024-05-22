using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MetroClimate.Services.Services;

public interface IStationService
{
    Task <List<StationDto>?> GetUserStationsAsync(int id);
    Task AddStationAsync(Station station);
    Task <bool> StationExists(string id);
}

public class StationService : IStationService
{
    private readonly MetroClimateDbContext _dbContext;
    
    public StationService(MetroClimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<StationDto>?> GetUserStationsAsync(int userId)
    {
        var station = await _dbContext.Stations
            .Include(s => s.Sensors)!
            .ThenInclude(s => s.Readings)
            .Include(s => s.Sensors)!
            .ThenInclude(s => s.SensorType)
            .Where(s => s.UserId == userId)
            .ToListAsync();
        
        var stationDtos = station.Select(s => new StationDto(s)).ToList();
        
        return stationDtos;
        
    }
    
    public async Task AddStationAsync(Station station)
    {
        await _dbContext.Stations.AddAsync(station);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<bool> StationExists(string id)
    {
        return await _dbContext.Stations.AnyAsync(s => s.Id == id);
    }
}