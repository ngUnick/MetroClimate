using MetroClimate.Data.Constants;
using MetroClimate.Data.Models;

namespace MetroClimate.Data.Dtos;

public class BaseSensorDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Unit { get; set; }
    public string? Symbol { get; set; }
    public SensorTypeEnum Type { get; set; }
    public double LastReading { get; set; }
    public bool Online { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public BaseSensorDto(Sensor sensor, double lastReading)
    {
        Id = sensor.Id;
        Name = sensor.Name;
        Description = sensor.Description;
        Unit = sensor.SensorType.Unit;
        Type = sensor.SensorType.SensorTypeEnum;
        Symbol = sensor.SensorType.Symbol;
        Online = sensor.LastReceived >= DateTime.UtcNow.AddSeconds(-15);
        LastReading = lastReading;
        Created = sensor.Created;
        Updated = sensor.Updated;
    }
    
    public BaseSensorDto(Sensor sensor)
    {
        Id = sensor.Id;
        Name = sensor.Name;
        Description = sensor.Description;
        Unit = sensor.SensorType.Unit;
        Type = sensor.SensorType.SensorTypeEnum;
        Online = sensor.LastReceived >= DateTime.UtcNow.AddSeconds(-15);
        Symbol = sensor.SensorType.Symbol;
        Created = sensor.Created;
        Updated = sensor.Updated;
    }
}