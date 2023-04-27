using System.Net.WebSockets;
using System.Text;
using WebSocket.Gateway;

public class WebSocketClient : IWebSocketClient
{
    private readonly Uri _url;
    private ClientWebSocket _socket;

    public WebSocketClient(string url)
    {
        _url = new Uri(url);
    }

    public async Task ConnectAsync()
    {
        _socket = new ClientWebSocket();

        await _socket.ConnectAsync(_url, CancellationToken.None);

        Console.WriteLine($"WebSocket connected to {_url}");

        await ReceiveAsync();
    }

    public async Task CloseAsync()
    {
        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }

    private async Task ReceiveAsync()
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
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                // Handle incoming data
                Console.WriteLine($"Received message: {message}");
            }
        }
    }
}
