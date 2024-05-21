using MetroClimate.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace MetroClimate.Services.Services;

public interface IUserService
{
    Task<string?> Login(string username, string password);
    
}

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly MetroClimateDbContext _dbContext;
    
    public UserService(IJwtService jwtService, MetroClimateDbContext dbContext)
    {
        _jwtService = jwtService;
        _dbContext = dbContext; 
    }
    
    public async Task<string?> Login(string username, string password)
    {
        //check if user exists
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        
        if (user == null)
        {
            return null;
        }
        
        return await _jwtService.GenerateJwtToken(user.Id);
        
    }
    
}