using System.Reflection;
using System.Text;
using FluentValidation;
using MetroClimate.Data.Configurations;
using MetroClimate.Data.Database;
using MetroClimate.Data.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MetroClimate.Services.Extensions;
using MetroClimate.Services.Services;
using Newtonsoft.Json;
using StackExchange.Redis;
using MetroClimate.Data.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MetroClimateDbContext>(options =>
    options.UseNpgsql(connectionString: builder.Configuration.GetConnectionString(name: "DefaultConnection"),
            x => x.MigrationsHistoryTable(tableName: "migrations_history",
                schema: builder.Configuration.GetConnectionString(name: "Schema")))
        .UseSnakeCaseNamingConvention());
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!));


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings!.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });



builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ExceptionFilter>();
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = BaseFirstContractResolver.Instance;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<IStationService, StationService>();
builder.Services.AddTransient<IReadingService, ReadingService>();
builder.Services.AddTransient<DataSeeder>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Add row to the database
    // using var scope = app.Services.CreateScope();
    // var dbContext = scope.ServiceProvider.GetRequiredService<MetroClimateDbContext>();
    // dbContext.WeatherForecasts.Add(new WeatherForecast
    // {
    //     TemperatureC = 20,
    //     Summary = "Sunny"
    // });
    //
    // await dbContext.SaveChangesAsync();
    using var scope = app.Services.CreateScope();
    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await dataSeeder.Seed();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
