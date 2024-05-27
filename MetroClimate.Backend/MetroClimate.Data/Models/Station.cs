using System.ComponentModel.DataAnnotations;
using MetroClimate.Data.Common;

namespace MetroClimate.Data.Models;

public class Station : IRecordable
{
    public required string Id { get; set; }
    public int UserId { get; set; }
    [MaxLength(20)]
    public required string Name { get; set; }
    [MaxLength(50)]
    public string? Description { get; set; }
    public List<Sensor>? Sensors { get; set; }
    public User User { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public DateTime LastReceived { get; set; }
}