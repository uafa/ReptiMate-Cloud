using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new WebSocketClient("wss://iotnet.teracom.dk/app?token=vnoUeQAAABFpb3RuZXQudGVyYWNvbS5kayL9sv9it8LFL5jggp-rve4=");

        try
        {
            await client.ConnectAsync();

            Console.WriteLine("Press any key to disconnect");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to WebSocket: {ex.Message}");
        }
        finally
        {
            await client.CloseAsync();
        }
    }
}