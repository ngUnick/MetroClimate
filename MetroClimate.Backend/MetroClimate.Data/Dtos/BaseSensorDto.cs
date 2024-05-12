using MetroClimate.Data.Models;

namespace MetroClimate.Data.Dtos;

public class BaseSensorDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Unit { get; set; }
    public string? Symbol { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public BaseSensorDto(Sensor sensor)
    {
        Id = sensor.Id;
        Name = sensor.Name;
        Description = sensor.Description;
        Unit = sensor.SensorType.Unit;
        Symbol = sensor.SensorType.Symbol;
        Created = sensor.Created;
        Updated = sensor.Updated;
    }
}