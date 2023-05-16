namespace WebSocket.Services;

public interface IMeasurementsServiceWS
{
    public Task SendMeasurements(string data);
}