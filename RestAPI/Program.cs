using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Repository;
using ReptiMate_Cloud.Services;
using ReptiMate_Cloud.Services.Auth;
using RestDAOs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] " +
            "and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SecurityTokenValidators.Clear();
    o.SecurityTokenValidators.Add(
        new GoogleTokenValidatorService("756576377617-0t412r5o9fepmnsso6utp40vgbgdfipg.apps.googleusercontent.com"));
});

builder.Services.AddHttpClient();

builder.Services.AddScoped<IMeasurementsServiceRest, MeasurementsServiceRest>();
builder.Services.AddScoped<IRestMeasurementsDAO, RestMeasurementsDAO>();

builder.Services.AddScoped<ITerrariumServiceRest, TerrariumServiceRest>();
builder.Services.AddScoped<IRestTerrariumDAO, RestTerrariumDAO>();

builder.Services.AddScoped<INotificationsService, NotificationServiceRest>();
builder.Services.AddScoped<IRestNotificationDAO, RestNotificationDAO>();

builder.Services.AddScoped<IAccountServiceRest, AccountServiceRest>();
builder.Services.AddScoped<IRestAccountDAO, RestAccountDAO>();

builder.Services.AddScoped<IAnimalServiceRest, AnimalServiceRest>();
builder.Services.AddScoped<IRestAnimalDAO, RestAnimalDAO>();

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