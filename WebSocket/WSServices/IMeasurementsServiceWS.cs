namespace WebSocket.Services;

public interface IMeasurementsServiceWS
{
    public Task ReceiveMeasurementsAsync(string data);
}