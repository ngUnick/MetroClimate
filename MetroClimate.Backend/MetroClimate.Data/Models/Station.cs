using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Common;

namespace MetroClimate.Data.Models;

public class Station : IRecordable
{
    public int Id { get; set; }
    public int UserId { get; set; }
    [MaxLength(10)]
    public required string Name { get; set; }
    [MaxLength(50)]
    public required string Description { get; set; }
    public List<Sensor>? Sensors { get; set; }
    public List<StationReading>? Readings { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}