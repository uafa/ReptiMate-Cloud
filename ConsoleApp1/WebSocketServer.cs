using System.Net.WebSockets;

namespace ConsoleApp1;

public class WebSocketServer
{
    public async Task Start(string url)
    {
        var listener = new WebSocketListener(url);
        Console.WriteLine($"Listening on {url}");

        while (true)
        {
            var webSocket = await listener.AcceptWebSocketAsync();
            Console.WriteLine("WebSocket connected");

            // start a new thread to handle the WebSocket
            var thread = new Thread(() => HandleWebSocket(webSocket));
            thread.Start();
        }
    }

    private void HandleWebSocket(WebSocket webSocket)
    {
        try
        {
            var buffer = new byte[1024];
            while (webSocket.State == WebSocketState.Open)
            {
                /*var result = webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.Result.MessageType == WebSocketMessageType.Text)
                {
                    var message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Result.Count);
                    Console.WriteLine($"Received message: {message}");

                    // echo the message back to the client
                    var messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
                    webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None); 
                }*/
                
                string message = @"
{
    ""cmd"": ""rx"",
    ""seqno"": 2746,
    ""EUI"": ""0004A30B00E7E212"",
    ""ts"": 1682681047297,
    ""fcnt"": 8,
    ""port"": 2,
    ""freq"": 867100000,
    ""rssi"": -116,
    ""snn"": -13,
    ""toa"": 0,
    ""dr"": ""SF12 BW125 4/5"",
    ""ack"": false,
    ""bat"": 255,
    ""offline"": false,
    ""data"": ""1538007b0028""
}";
                var messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
                webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                Thread.Sleep(60000);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"WebSocket error: {ex.Message}");
        }
        finally
        {
            webSocket.Dispose();
            Console.WriteLine("WebSocket disconnected");
        }
    }
}