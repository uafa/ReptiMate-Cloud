using System.Net.WebSockets;
using System.Text;
using Model;
using Newtonsoft.Json.Linq;
using WebSocket;
using WebSocket.Gateway;
using WebSocket.Services;

public class WebSocketClient : IWebSocketClient
{
    private readonly Uri _url;
    private ClientWebSocket _socket;
    private readonly IMeasurementsServiceWS measurementsService;
    private readonly ITerrariumServiceWS terrariumService;
    private DataConvertor dataConvertor;

    public WebSocketClient(string url, IMeasurementsServiceWS measurementsService, ITerrariumServiceWS terrariumService)
    {
        _url = new Uri(url);
        this.measurementsService = measurementsService;
        this.terrariumService = terrariumService;
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

        var currentLimits = await terrariumService.GetTerrariumLimitsAsync();

        await SendConfigurationAsync(currentLimits);
        
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
                    await measurementsService.SendMeasurementsAsync(convertedData);
                }
            }

            TerrariumLimits possibleNewLimits = await terrariumService.GetTerrariumLimitsAsync();

            Console.WriteLine("Possible new limits: Max: " + possibleNewLimits.TemperatureLimitMax + " Min: " +
                              possibleNewLimits.TemperatureLimitMin);


            if (CompareLimits(currentLimits, possibleNewLimits))
            {
                Console.WriteLine("Current Limits: Max: " + currentLimits.TemperatureLimitMax + " Min: " +
                                  currentLimits.TemperatureLimitMin);

                await SendConfigurationAsync(possibleNewLimits);

                Console.WriteLine("Possible new limits sent: Max: " + possibleNewLimits.TemperatureLimitMax + " Min: " +
                                  possibleNewLimits.TemperatureLimitMin);

                currentLimits = possibleNewLimits;

                Console.WriteLine("Current Limits: Max: " + currentLimits.TemperatureLimitMax + " Min: " +
                                  currentLimits.TemperatureLimitMin);
            }
        }
    }

    private bool CompareLimits(TerrariumLimits currentLimits, TerrariumLimits possibleNewLimits)
    {
        return currentLimits.TemperatureLimitMax != possibleNewLimits.TemperatureLimitMax ||
               currentLimits.TemperatureLimitMin != possibleNewLimits.TemperatureLimitMin;
    }


    public async Task SendConfigurationAsync(TerrariumLimits terrariumLimits)
    {
        if (_socket.State != WebSocketState.Open)
            throw new Exception("WebSocket connection has not been established");

        Console.WriteLine("Preparing to send data.");

        var terrariumLimitsInHexa = dataConvertor.ConvertTemperatureLimitsToHex(terrariumLimits);

        Console.WriteLine("Converted data to hex: " + terrariumLimitsInHexa);
        
        var jsonObject = new JObject(
            new JProperty("cmd", "tx"),
            new JProperty("EUI", "0004A30B00E7E212"),
            new JProperty("port", "2"),
            new JProperty("confirmed", false),
            new JProperty("data", terrariumLimitsInHexa)
        );
        
        var buffer = Encoding.UTF8.GetBytes(jsonObject.ToString());

        await _socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true,
            CancellationToken.None);
        Console.WriteLine("Data sent.");
    }

    public async Task CloseAsync()
    {
        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }
}