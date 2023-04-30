using System.Net;
using System.Net.WebSockets;

namespace ConsoleApp1;

public class WebSocketListener
{
    private readonly HttpListener listener;

    public WebSocketListener(string url)
    {
        listener = new HttpListener();
        listener.Prefixes.Add(url);
        listener.Start();
    }

    public async Task<WebSocket> AcceptWebSocketAsync()
    {
        var context = await listener.GetContextAsync();
        if (!context.Request.IsWebSocketRequest)
        {
            context.Response.StatusCode = 400;
            context.Response.Close();
            return null;
        }

        var webSocketContext = await context.AcceptWebSocketAsync(null);
        return webSocketContext.WebSocket;
    }
}