using Microsoft.EntityFrameworkCore;
using ScheduleCore.Models;
using ScheduleCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Add CoreService to the container.
builder.Services.AddSingleton<ICoreService, CoreService>();

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