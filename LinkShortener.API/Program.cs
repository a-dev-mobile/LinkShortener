using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using LinkShortener.Data;



var builder = WebApplication.CreateBuilder(args);



// Настройка логирования
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Добавление и настройка контекста базы данных
builder.Services.AddDbContext<LinkShortenerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавление сервисов для контроллеров
builder.Services.AddControllers();

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


app.UseCors("AllowAll");
app.UsePathBase("/linkshortener");
app.UseForwardedHeaders();
app.UseSwagger();
app.UseSwaggerUI(options => { options.SwaggerEndpoint("/linkshortener/swagger/v1/swagger.json", "My API V1"); });


// Установка порта прослушивания на 80
if (!app.Environment.IsDevelopment())
{
    app.Urls.Add("http://*:80");
}

// app.UseAuthorization();

// Регистрация маршрутов для контроллеров
app.MapControllers(); 

app.Run();