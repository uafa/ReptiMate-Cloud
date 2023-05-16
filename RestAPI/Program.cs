using System.Data.SqlClient;
using Repository;
using ReptiMate_Cloud.Services;
using RestDAOs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IMeasurementsServiceRest, MeasurementsServiceRest>();
builder.Services.AddScoped<IRestMeasurementsDAO, RestMeasurementsDAO>();



builder.Services.AddScoped<ITerrariumServiceRest, TerrariumServiceRest>();
builder.Services.AddScoped<IRestTerrariumDAO, RestTerrariumDAO>();

builder.Services.AddScoped<INotificationsService, NotificationServiceRest>();
builder.Services.AddScoped<IRestNotificationDAO, RestNotificationDAO>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountDAO, AccountDAO>();


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
