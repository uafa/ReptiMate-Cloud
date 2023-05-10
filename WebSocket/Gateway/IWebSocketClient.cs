using Model;

namespace WebSocket.Gateway;

public interface IWebSocketClient
{
    public Task ConnectAsync();
    public Task CloseAsync();
    public Task SendConfigurationAsync(String terrariumLimitsInHexa);
}