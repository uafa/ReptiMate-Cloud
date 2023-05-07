using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json.Linq;
using WebSocket;
using WebSocket.Gateway;
using WebSocket.Services;

public class WebSocketClient : IWebSocketClient
{
    private readonly Uri _url;
    private ClientWebSocket _socket;
    private readonly IMeasurementsServiceWS measurementsService;
    private DataConvertor dataConvertor;

    public WebSocketClient(string url, IMeasurementsServiceWS measurementsService)
    {
        _url = new Uri(url);
        this.measurementsService = measurementsService;
        dataConvertor = new DataConvertor();
    }

    public async Task ConnectAsync()
    {
        _socket = new ClientWebSocket();

        await _socket.ConnectAsync(_url, CancellationToken.None);

        Console.WriteLine($"WebSocket connected to {_url}");
    }

    public async Task StartReceivingAsync()
    {
        var buffer = new byte[1024];

        while (_socket.State == WebSocketState.Open)
        {
            var result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);


            if (result.MessageType == WebSocketMessageType.Close)
            {
                await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            else
            {
                var data = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($" WebSocketClient: Received data: {data}");

                // Handle incoming data

                var jObject = JObject.Parse(data);

                if (jObject["cmd"].Value<string>() == "rx")
                {
                    Console.WriteLine($" WebSocketClient: Received data: ");
                    var convertedData = dataConvertor.GetData(data);
                    measurementsService.SendMeasurements(convertedData);
                }
            }
        }
    }

    public async Task CloseAsync()
    {
        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }
}