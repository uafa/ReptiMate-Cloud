namespace WebSocket.Gateway;

public interface IWebSocketClient
{
    public Task ConnectAsync();
    public Task CloseAsync();
}