using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using LinkShortener.Data;
using LinkShortener.Data.Interfaces;
using LinkShortener.Data.Repositories;
using LinkShortener.Service.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;




var builder = WebApplication.CreateBuilder(args);

// Настройка логирования
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Добавление и настройка контекста базы данных
builder.Services.AddDbContext<LinkShortenerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Регистрация  в DI контейнере
builder.Services.AddScoped<ILinkRepository, LinkRepository>();
builder.Services.AddScoped<ILinkShortenerService, LinkShortenerService>();


// Добавление сервисов для контроллеров
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new OpenApiInfo
    {

        Version = "v1",
        Title = "Link Shortener API",
        Description = "API for shortening links",
        Contact = new OpenApiContact
        {
            Name = "Dmitriy Trofimov",
            Email = "wayofdt@gmail.com",

        },

    });
});

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


app.UseCors("AllowAll");
app.UsePathBase("/linkshortener");
app.UseForwardedHeaders();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/linkshortener/swagger/v1/swagger.json", "My API V1");
});

// Установка порта прослушивания на 80
if (!app.Environment.IsDevelopment())
{
    app.Urls.Add("http://*:80");

}




var IsDevelopment = app.Environment.IsDevelopment();
var appEnvironment = IsDevelopment ? "3Development" : "3Production";
app.Logger.LogInformation("Приложение запущено");
app.Logger.LogInformation($"AppEnvironment: {appEnvironment}");

// Регистрация маршрутов для контроллеров
app.MapControllers();

app.Run();