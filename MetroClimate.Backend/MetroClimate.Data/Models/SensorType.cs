using System.ComponentModel.DataAnnotations;

namespace MetroClimate.Data.Models;

public class SensorType
{
    public int Id { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(50)]
    public required string Description { get; set; }
    [MaxLength(10)]
    public required string Unit { get; set; }
    [MaxLength(10)]
    public required string Symbol { get; set; }
    public required int MinValue { get; set; }
    public required int MaxValue { get; set; }
}