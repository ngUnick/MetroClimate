using MetroClimate.Data.Database;
using MetroClimate.Data.Dtos;
using MetroClimate.Data.Dtos.Payload;
using MetroClimate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MetroClimate.Services.Services;

public interface IReadingService
{
    Task<List<FullStationReadingDto>?> GetReadingsAsync(int userId, int sensorId, int? minutes = null, int? groupByMinutes = null);
    Task RecordReadingAsync(StationReadingPld reading);
}

public class ReadingService : IReadingService
{
    private MetroClimateDbContext _dbContext;
    
    public ReadingService(MetroClimateDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<FullStationReadingDto>?> GetReadingsAsync(int userId, int sensorId, int? minutes = null , int? groupByMinutes = null)
    {
        var oneDayAgo = DateTime.UtcNow.AddMinutes(-minutes ?? -1440);

        var query = _dbContext.StationReadings
            .Include(sr => sr.Station)
            .Include(sr => sr.Sensor)
            .ThenInclude(s => s.SensorType)
            .Where(sr => sr.Sensor.Station.UserId == userId && sr.SensorId == sensorId && sr.Created >= oneDayAgo)
            .OrderBy(sr => sr.Created)
            .AsQueryable();

        if (groupByMinutes.HasValue)
        {
            query = query
                .GroupBy(sr => new
                {
                    sr.Created.Year,
                    sr.Created.Month,
                    sr.Created.Day,
                    sr.Created.Hour,
                    GroupByMinutes = sr.Created.Minute / groupByMinutes.Value
                })
                .Select(g => new StationReading
                {
                    Id = g.First().Id,
                    Value = g.Average(sr => sr.Value),
                    Created = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day, g.Key.Hour, g.Key.GroupByMinutes * groupByMinutes.Value, 0),
                    StationId = g.First().StationId,
                    SensorId = g.First().SensorId,
                    Sensor = g.First().Sensor, // Ensure Sensor is included
                    Station = g.First().Station // Ensure Station is included
                })
                .OrderBy(sr => sr.Created)
                .AsQueryable();
        }

        var readings = await query.ToListAsync();

        
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
        sensor.LastReceived = DateTime.UtcNow;
        _dbContext.Stations.Update(sensor.Station);
        await _dbContext.SaveChangesAsync();
    }
    
}