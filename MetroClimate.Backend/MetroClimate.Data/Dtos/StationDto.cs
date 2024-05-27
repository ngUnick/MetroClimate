using MetroClimate.Data.Models;

namespace MetroClimate.Data.Dtos;

public class StationDto
{
    public string Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Online { get; set; } 
    public List<BaseSensorDto>? Sensors { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public StationDto(Station station, List<StationReading?> latestValues)
    {
        Id = station.Id;
        UserId = station.UserId;
        Name = station.Name;
        Description = station.Description;
        Sensors = station.Sensors?.Select(sensor =>
        {
            var lastReading = latestValues.FirstOrDefault(sr => sr != null && sr.SensorId == sensor.Id)?.Value ?? 0;
            return new BaseSensorDto(sensor, lastReading);
        }).ToList();
        Created = station.Created;
        Updated = station.Updated;
        Online = station.LastReceived >= DateTime.UtcNow.AddSeconds(-15);
    }
    
}