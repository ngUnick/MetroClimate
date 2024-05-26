namespace MetroClimate.Data.Dtos.Payload;

public class GetSensorReadingsPld
{
    public int SensorId { get; set; }
    public int? Minutes { get; set; }
    public int? GroupByMinutes { get; set; }
}