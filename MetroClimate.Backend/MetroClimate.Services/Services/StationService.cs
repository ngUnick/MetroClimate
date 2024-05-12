using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MetroClimate.Services.Services;

public interface IStationService
{
    Task <List<StationDto>?> GetUserStationsAsync(int id);
    Task RecordReadingAsync(StationReadingPld reading);
}

public class StationService : IStationService
{
    private MetroClimateDbContext _dbContext;
    
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
    
    public async Task RecordReadingAsync(StationReadingPld reading)
    {
        var sensor = await _dbContext.Sensors.FindAsync(reading.SensorId);
        if(sensor == null)
        {
            var sensorType = await _dbContext.SensorTypes.FirstOrDefaultAsync(st => st.SensorTypeEnum == reading.SensorType);
            if (sensorType == null)
            {
                throw new Exception("Sensor type not found");
            }
            
            
            sensor = new Sensor()
            {
                Id = reading.SensorId,
                SensorType = sensorType,
                Name = reading.SensorName,
                StationId = (int)reading.StationId
            };
            _dbContext.Sensors.Add(sensor);
        }
        
        var stationReading = new StationReading()
        {
            StationId = (int)reading.StationId,
            SensorId = reading.SensorId,
            Value = reading.Value
        };
        
        _dbContext.StationReadings.Add(stationReading);
        await _dbContext.SaveChangesAsync();
    }
    
}