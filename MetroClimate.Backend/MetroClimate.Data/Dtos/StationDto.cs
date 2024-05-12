using MetroClimate.Data.Models;

namespace MetroClimate.Data.Dtos;

public class StationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<SensorDto>? Sensors { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    
    public StationDto(Station station)
    {
        Id = station.Id;
        UserId = station.UserId;
        Name = station.Name;
        Description = station.Description;
        Sensors = station.Sensors?.Select(sensor => new SensorDto(sensor)).ToList();
        Created = station.Created;
        Updated = station.Updated;
    }
    
}