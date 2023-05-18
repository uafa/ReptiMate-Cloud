namespace WebSocket.Services;

public interface IMeasurementsServiceWS
{
    public Task SendMeasurementsAsync(string data);
}