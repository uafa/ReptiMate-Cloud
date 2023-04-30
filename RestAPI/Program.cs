using System.Data.SqlClient;
using Repository;
using Repository.DAO;
using WebSocket.Services;
using ReptiMate_Cloud.Services;
using WebSocket.Gateway;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMeasurementsServiceRest, MeasurementsServiceRest>();
builder.Services.AddScoped<IMeasurementsServiceWS, MeasurementsServiceWS>();
builder.Services.AddScoped<IMeasurementsDAO, MeasurementsDAO>();

builder.Services.AddDbContext<DatabaseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
