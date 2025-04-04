using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Constants;

namespace MetroClimate.Data.Dtos.Payload;

public class StationReadingPld
{
    [Required]
    public required string StationId { get; set; }
    [Required]
    public required int SensorId { get; set; }
    [Required]
    public required string SensorName { get; set; }
    [Required]
    public required SensorTypeEnum SensorType { get; set; }
    [Required]
    public required double Value { get; set; }
}