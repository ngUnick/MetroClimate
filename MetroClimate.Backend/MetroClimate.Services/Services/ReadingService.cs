using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Dtos.Payload;
using MetroClimate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MetroClimate.Services.Services;

public interface IReadingService
{
    Task<List<FullStationReadingDto>?> GetReadingsAsync(int userId, int sensorId);
    Task RecordReadingAsync(StationReadingPld reading);
}

public class ReadingService : IReadingService
{
    private MetroClimateDbContext _dbContext;
    
    public ReadingService(MetroClimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<FullStationReadingDto>?> GetReadingsAsync(int userId, int sensorId)
    {
        var readings = await _dbContext.StationReadings
            .Include(sr => sr.Station)
            .Include(sr => sr.Sensor)
            .ThenInclude(s => s.SensorType)
            .Where(sr => sr.Sensor.Station.UserId == userId && sr.SensorId == sensorId)
            .OrderByDescending(sr => sr.Created)
            .ToListAsync();
        
        return readings.Select(sr => new FullStationReadingDto(sr)).ToList();
        
    }
    
    public async Task RecordReadingAsync(StationReadingPld reading)
    {
        var sensor = await _dbContext.Sensors.Include(s => s.Station).FirstOrDefaultAsync(s => s.Id == reading.SensorId);
        if(sensor == null)
        {
            var sensorType = await _dbContext.SensorTypes.FirstOrDefaultAsync(st => st.SensorTypeEnum == reading.SensorType);
            if (sensorType == null)
            {
                throw new Exception("Sensor type not found");
            }
            
            
            sensor = new Sensor
            {
                Id = reading.SensorId,
                SensorType = sensorType,
                Name = reading.SensorName,
                StationId = reading.StationId,
                Station = (await _dbContext.Stations.FirstOrDefaultAsync(s => s.Id == reading.StationId))!
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
        sensor.Station.LastReceived = DateTime.UtcNow;
        _dbContext.Stations.Update(sensor.Station);
        await _dbContext.SaveChangesAsync();
    }
    
}