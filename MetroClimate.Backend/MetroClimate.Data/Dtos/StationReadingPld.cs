using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Constants;

namespace MetroClimate.Data.Dtos;

public class StationReadingPld
{
    [Required]
    public int? StationId { get; set; }
    [Required]
    public int? SensorId { get; set; }
    [Required]
    public SensorTypeEnum? SensorType { get; set; }
    [Required]
    public double? Value { get; set; }
}