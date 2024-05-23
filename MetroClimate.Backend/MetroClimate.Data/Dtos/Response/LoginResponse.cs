namespace MetroClimate.Data.Dtos.Response;

public class LoginResponse
{
    public string Token { get; set; }
    public UserDto User { get; set; }
}