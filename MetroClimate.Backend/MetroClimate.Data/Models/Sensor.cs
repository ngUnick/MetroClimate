using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Common;
using MetroClimate.Data.Constants;

namespace MetroClimate.Data.Models;

public class Sensor : IRecordable
{
    public int Id { get; set; }
    [MaxLength(10)]
    public required string StationId { get; set; }
    public int SensorTypeId { get; set; }
    [MaxLength(15)]
    public string? Name { get; set; }
    [MaxLength(30)]
    public string? Description { get; set; }

    #region Navigation Properties
    public Station Station { get; set; } = null!;
    public SensorType SensorType { get; set; } = null!;
    public List<StationReading>? Readings { get; set; }
    
    #endregion
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}