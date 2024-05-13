using MetroClimate.Data.Common;

namespace MetroClimate.Data.Models;

public class Users : IRecordable
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}