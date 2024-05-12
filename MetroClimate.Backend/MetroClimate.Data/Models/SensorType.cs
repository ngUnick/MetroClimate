using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Constants;

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
    public SensorTypeEnum SensorTypeEnum { get; set; }
    
    // Property to store the conversion formula
    [MaxLength(255)]
    public string? Formula { get; set; }
    
    public required int MinValue { get; set; }
    public required int MaxValue { get; set; }
    
}