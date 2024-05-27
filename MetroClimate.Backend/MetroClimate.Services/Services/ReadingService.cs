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
    public async Task<List<FullStationReadingDto>?> GetReadingsAsync(int userId, int sensorId, int? minutes = null, int? groupByMinutes = null)
    {
        var startDate = DateTime.UtcNow.AddMinutes(-minutes ?? -1440);

        var query = _dbContext.StationReadings
            .Include(sr => sr.Station)
            .Include(sr => sr.Sensor)
            .ThenInclude(s => s.SensorType)
            .Where(sr => sr.Sensor.Station.UserId == userId && sr.SensorId == sensorId && sr.Created >= startDate)
            .OrderBy(sr => sr.Created)
            .AsQueryable();

        var readings = await query.ToListAsync();

        if (groupByMinutes.HasValue && readings.Any())
        {
            int interval = groupByMinutes.Value;
            DateTime firstReadingTime = readings.First().Created;
            DateTime intervalStartTime = new DateTime(
                firstReadingTime.Year,
                firstReadingTime.Month,
                firstReadingTime.Day,
                firstReadingTime.Hour,
                (firstReadingTime.Minute / interval) * interval,
                0,
                firstReadingTime.Kind
            );

            if (intervalStartTime > firstReadingTime)
            {
                intervalStartTime = intervalStartTime.AddMinutes(-interval);
            }

            var groupedReadings = readings
                .GroupBy(sr => new DateTime(
                    intervalStartTime.Ticks + (((sr.Created.Ticks - intervalStartTime.Ticks) / TimeSpan.FromMinutes(interval).Ticks) * TimeSpan.FromMinutes(interval).Ticks),
                    DateTimeKind.Utc))
                .Select(g => new
                {
                    IntervalStart = g.Key,
                    AverageValue = g.Average(sr => sr.Value),
                    RepresentativeReading = g.First() // Use the first reading to get other properties like Unit, Symbol, etc.
                })
                .OrderBy(g => g.IntervalStart)
                .ToList();

            var result = groupedReadings.Select(g => new FullStationReadingDto(g.RepresentativeReading)
            {
                Value = g.AverageValue,
                Created = g.IntervalStart
            }).ToList();

            return result;
        }

        return readings.Select(sr => new FullStationReadingDto(sr)).ToList();
    }




    // public async Task<List<FullStationReadingDto>?> GetReadingsAsync(int userId, int sensorId, int? minutes = null, int? groupByMinutes = null)
    // {
    //     var startDate = DateTime.UtcNow.AddMinutes(-minutes ?? -1440);
    //
    //     var query = _dbContext.StationReadings
    //         .Include(sr => sr.Station)
    //         .Include(sr => sr.Sensor)
    //         .ThenInclude(s => s.SensorType)
    //         .Where(sr => sr.Sensor.Station.UserId == userId && sr.SensorId == sensorId && sr.Created >= startDate)
    //         .OrderBy(sr => sr.Created)
    //         .AsQueryable();
    //
    //     var readings = await query.ToListAsync();
    //
    //     if (groupByMinutes.HasValue)
    //     {
    //         int interval = groupByMinutes.Value;
    //
    //         var groupedReadings = readings
    //             .GroupBy(sr => new DateTime(
    //                 sr.Created.Year,
    //                 sr.Created.Month,
    //                 sr.Created.Day,
    //                 sr.Created.Hour,
    //                 (sr.Created.Minute / interval) * interval,
    //                 0,
    //                 DateTimeKind.Utc))
    //             .Select(g => new
    //             {
    //                 IntervalStart = g.Key,
    //                 AverageValue = g.Average(sr => sr.Value),
    //                 RepresentativeReading = g.First() // We use the first reading to get other properties like Unit, Symbol, etc.
    //             })
    //             .OrderBy(g => g.IntervalStart)
    //             .ToList();
    //
    //         var result = groupedReadings.Select(g => new FullStationReadingDto(g.RepresentativeReading)
    //         {
    //             Value = g.AverageValue,
    //             Created = g.IntervalStart
    //         }).ToList();
    //
    //         return result;
    //     }
    //
    //     return readings.Select(sr => new FullStationReadingDto(sr)).ToList();
    // }










    
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