var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS services and define the "AllowAll" policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Use CORS with the "AllowAll" policy
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();


var testValue = builder.Configuration.GetValue<string>("TestValue");
var appEnvironment = app.Environment.IsDevelopment()?"Development":"Production";

// Log testValue and appEnvironment
app.Logger.LogInformation($"TestValue: {testValue}");
app.Logger.LogInformation($"AppEnvironment: {appEnvironment}");




var summaries = new[]





{
    $"{testValue}", $"{testValue}", $"{testValue}",$"{appEnvironment}", $"{appEnvironment}", $"{appEnvironment}", $"{appEnvironment}", "Hot", "Sweltering", "Scorching"
};
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
