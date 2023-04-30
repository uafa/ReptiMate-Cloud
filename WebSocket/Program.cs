using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.DAO;
using WebSocket.Services;

class Program
{
    static async Task Main(string[] args)
    {
        await Start();
    }

    public static async Task Start()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddScoped<IMeasurementsServiceWS, MeasurementsServiceWS>();
        services.AddScoped<IMeasurementsDAO, MeasurementsDAO>();
        services.AddDbContext<DatabaseContext>();

        var serviceProvider = services.BuildServiceProvider();

        if (!serviceProvider.GetRequiredService<DatabaseContext>().Database.CanConnect())
        {
            Console.WriteLine("Did not connect");
            return;
        }

        // url for testing
        var client = new WebSocketClient("ws://localhost:8080/",
            serviceProvider.GetRequiredService<IMeasurementsServiceWS>());

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