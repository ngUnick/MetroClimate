using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Constants;

namespace MetroClimate.Data.Dtos;

public class StationReadingPld
{
    public int StationId { get; set; }
    public int SensorId { get; set; }
    public SensorTypeEnum SensorType { get; set; }
    public double Value { get; set; }
}