using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Common;

namespace MetroClimate.Data.Models;

public class Sensor : IRecordable
{
    public int Id { get; set; }
    public int StationId { get; set; }
    public int SensorTypeId { get; set; }
    [MaxLength(10)]
    public required string Name { get; set; }
    [MaxLength(30)]
    public required string Description { get; set; }
   

    #region Navigation Properties
    public Station? Station { get; set; }
    public SensorType SensorType { get; set; } = null!;
    
    #endregion
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}