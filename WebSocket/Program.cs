﻿using Microsoft.Extensions.DependencyInjection;
using Repository;
using WebSocket.Services;
using WSDAOs;

class Program
{
    static async Task Main(string[] args)
    {
        await Start();
    }

    private static async Task Start()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddScoped<IMeasurementsServiceWS, MeasurementsServiceWS>();
        services.AddScoped<IWSMeasurementsDAO, WSMeasurementsDAO>();
        services.AddScoped<IWSNotificationDAO, WSNotificationsDAO>();
        services.AddScoped<IWSBoundariesDAO, WSBoundariesDAO>();
        services.AddScoped<ITerrariumServiceWS, TerrariumServiceWS>();
        services.AddScoped<IWSTerrariumDAO, WSTerrariumDAO>();
        services.AddDbContext<DatabaseContext>();

        var serviceProvider = services.BuildServiceProvider();

        if (!serviceProvider.GetRequiredService<DatabaseContext>().Database.CanConnect())
        {
            Console.WriteLine("Did not connect");
            return;
        }

        var client = new WebSocketClient("wss://iotnet.teracom.dk/app?token=vnoUeQAAABFpb3RuZXQudGVyYWNvbS5kayL9sv9it8LFL5jggp-rve4=",
            serviceProvider.GetRequiredService<IMeasurementsServiceWS>(),
            serviceProvider.GetRequiredService<ITerrariumServiceWS>());

        try
        {
            await client.ConnectAsync();

            await client.StartReceivingAsync();

            Console.WriteLine("Press any key to disconnect");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to WebSocket:" + ex);
        }
        finally
        {
            await client.CloseAsync();
        }
    }
}