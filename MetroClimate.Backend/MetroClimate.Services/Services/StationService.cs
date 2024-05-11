using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MetroClimate.Services.Services;

public interface IStationService
{
    Task <List<Station>?> GetUserStationsAsync(int id);
    Task RecordReadingAsync(StationReadingPld reading);
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
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }
    
    public async Task RecordReadingAsync(StationReadingPld reading)
    {
        var sensor = await _dbContext.Sensors.FindAsync(reading.SensorId);
        if(sensor == null)
        {
            var sensorType = await _dbContext.SensorTypes.FirstOrDefaultAsync(st => st.SensorTypeEnum == reading.SensorType);
            sensor = new Sensor()
            {
                Id = reading.SensorId,
                SensorType = sensorType,
                StationId = reading.StationId
            };
            _dbContext.Sensors.Add(sensor);
        }
        
        var stationReading = new StationReading()
        {
            StationId = reading.StationId,
            SensorId = reading.SensorId,
            Value = reading.Value
        };
        
        _dbContext.StationReadings.Add(stationReading);
        await _dbContext.SaveChangesAsync();
    }
    
}