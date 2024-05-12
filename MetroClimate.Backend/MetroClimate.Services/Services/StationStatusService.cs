// using Microsoft.Extensions.Hosting;
// using System.Threading;
// using System.Threading.Tasks;
// using MetroClimate.Data.Database;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace MetroClimate.Services.Services;
//
// public class StationStatusService : BackgroundService
// {
//     private readonly IServiceScopeFactory _scopeFactory;
//
//     public StationStatusService(IServiceScopeFactory scopeFactory)
//     {
//         _scopeFactory = scopeFactory;
//     }
//
//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         while (!stoppingToken.IsCancellationRequested)
//         {
//             await CheckStationStatus();
//             await Task.Delay(10000, stoppingToken); // Check every 10 seconds
//         }
//     }
//
//     private async Task CheckStationStatus()
//     {
//         using var scope = _scopeFactory.CreateScope();
//         var dbContext = scope.ServiceProvider.GetRequiredService<MetroClimateDbContext>();
//         var stations = await dbContext.Stations.ToListAsync();
//         var cutoff = DateTime.UtcNow.AddSeconds(-30);
//         foreach (var station in stations)
//         {
//             if (station.LastReceived < cutoff && station.Online)
//             {
//                 station.Online = false;
//             }
//             else if (station.LastReceived >= cutoff && !station.Online)
//             {
//                 station.Online = true;
//             }
//             dbContext.Update(station);
//         }
//
//         await dbContext.SaveChangesAsync();
//     }
// }