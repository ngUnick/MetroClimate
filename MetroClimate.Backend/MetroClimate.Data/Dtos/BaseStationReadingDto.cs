using MetroClimate.Data.Models;
using MetroClimate.Data.Static;

namespace MetroClimate.Data.Dtos;

public class BaseStationReadingDto
{
    public int Id { get; set; }
    public double Value { get; set; }
    public DateTime Created { get; set; }
    
    public BaseStationReadingDto(StationReading reading)
    {
        Id = reading.Id;
        Value = reading.Value;
        Created = reading.Created;
    }
    
    public BaseStationReadingDto(StationReading reading, SensorType sensorType)
    {
        Id = reading.Id;
        Value = reading.Value;
        Created = reading.Created;
    }
}