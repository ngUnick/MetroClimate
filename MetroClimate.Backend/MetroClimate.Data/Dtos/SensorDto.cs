using MetroClimate.Data.Models;

namespace MetroClimate.Data.Dtos;

public class SensorDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public List<StationReadingDto>? Readings { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public SensorDto(Sensor sensor)
    {
        Id = sensor.Id;
        Name = sensor.Name;
        Description = sensor.Description;
        Readings = sensor.Readings?.Select(reading => new StationReadingDto(reading, sensor.SensorType)).ToList();
        Created = sensor.Created;
        Updated = sensor.Updated;
    }
}