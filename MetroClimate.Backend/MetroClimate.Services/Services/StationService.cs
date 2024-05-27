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
        // Step 1: Retrieve the stations and their sensors associated with the user
        var stations = await _dbContext.Stations
            .Include(s => s.Sensors)!
            .ThenInclude(s => s.SensorType)
            .Where(s => s.UserId == userId)
            .ToListAsync();

        // Step 2: Get the sensor IDs from the stations
        var sensorIds = stations
            .SelectMany(s => s.Sensors!)
            .Select(sensor => sensor.Id)
            .ToList();

        // Step 3: Retrieve the latest value for each sensor
        var latestValuesOfEachSensorOfEachStation = await _dbContext.StationReadings
            .Where(sr => sensorIds.Contains(sr.SensorId))
            .GroupBy(sr => sr.SensorId)
            .Select(g => g.OrderByDescending(sr => sr.Created).FirstOrDefault())
            .ToListAsync();


        
        var stationDtos = stations.Select(station =>
        {
            var latestValues = latestValuesOfEachSensorOfEachStation
                .Where(sr => station.Sensors!.Select(s => s.Id).Contains(sr.SensorId))
                .ToList();
            return new StationDto(station, latestValues);
        }).ToList();
        
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