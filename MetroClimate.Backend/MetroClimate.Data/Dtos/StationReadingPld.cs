using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Constants;

namespace MetroClimate.Data.Dtos;

public class StationReadingPld
{
    [Required]
    public required int StationId { get; set; }
    [Required]
    public required int SensorId { get; set; }
    [Required]
    public required string SensorName { get; set; }
    [Required]
    public required SensorTypeEnum SensorType { get; set; }
    [Required]
    public required double Value { get; set; }
}