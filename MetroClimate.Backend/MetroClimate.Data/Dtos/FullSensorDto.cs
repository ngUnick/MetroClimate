using MetroClimate.Data.Models;

namespace MetroClimate.Data.Dtos;

public class FullSensorDto : BaseSensorDto
{
    public List<FullStationReadingDto>? Readings { get; set; }
    
    public FullSensorDto(Sensor sensor) : base(sensor)
    {
        Readings = sensor.Readings?.Select(reading => new FullStationReadingDto(reading, sensor.SensorType)).ToList();
    }
}