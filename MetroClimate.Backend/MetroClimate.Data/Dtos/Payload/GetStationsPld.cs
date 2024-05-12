using System.ComponentModel.DataAnnotations;

namespace MetroClimate.Data.Dtos.Payload;

public class GetStationsPld
{
    [Required]
    public int UserId { get; set; }
}