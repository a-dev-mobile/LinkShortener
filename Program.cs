var builder = WebApplication.CreateBuilder(args);


// Настройка логирования
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug); // Или другой уровень по вашему выбору

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
var testValue = builder.Configuration.GetValue<string>("TestValue");
var IsDevelopment = app.Environment.IsDevelopment();
var appEnvironment = IsDevelopment ? "11Development" : "11Production";
app.UseCors("AllowAll");
app.UsePathBase("/linkshortener");
app.UseForwardedHeaders();
app.UseSwagger();


if (IsDevelopment)
{
    // В режиме разработки используйте Swagger UI с указанием пути к Swagger JSON
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/linkshortener/swagger/v1/swagger.json", "My API V1");
    });
}
else
{
    // В продуктивной среде (не в режиме разработки) с указанием пути к Swagger JSON
    app.UseSwaggerUI(options =>
    {
        // options.RoutePrefix = ""; // Сделайте Swagger UI доступным прямо на базовом пути
        options.SwaggerEndpoint("/linkshortener/swagger/v1/swagger.json", "My API V1");
    });
}
// Установка порта прослушивания на 80
if (!IsDevelopment)
    app.Urls.Add("http://*:80");


app.Logger.LogInformation("Приложение запущено");
app.Logger.LogInformation($"TestValue: {testValue}");
app.Logger.LogInformation($"AppEnvironment: {appEnvironment}");





var summaries = new[]





{
    $"{testValue}", $"{testValue}", $"{testValue}",$"{appEnvironment}", $"{appEnvironment}", $"{appEnvironment}", $"{appEnvironment}", "Hot", "Sweltering", "Scorching"
};
app.MapGet("/v1/weatherforecast", () =>
{
    app.Logger.LogInformation("Обработка запроса /linkshortener/weatherforecast");

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    app.Logger.LogInformation("Запрос /linkshortener/weatherforecast успешно обработан");

    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();



app.Run();
app.Logger.LogInformation("Приложение остановлено");

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
