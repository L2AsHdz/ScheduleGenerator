using Microsoft.EntityFrameworkCore;
using ScheduleCore.Models;
using ScheduleCore.Services.Core;
using ScheduleCore.Services.Parametro;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers to the container.
builder.Services.AddControllers();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add DbContext to the container.
var connectionString = builder.Configuration.GetConnectionString("ScheduleDB");
builder.Services.AddDbContext<ScheduleDB>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add Services to the container.
builder.Services.AddScoped<ParametroService>();
builder.Services.AddScoped<CoreService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();