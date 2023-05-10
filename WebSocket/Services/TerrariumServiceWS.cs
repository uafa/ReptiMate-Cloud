using Model;
using Repository.DAO;
using WebSocket.Gateway;

namespace WebSocket.Services;

public class TerrariumServiceWS : ITerrariumServiceWS, ITerrariumDAOEventHandler
{
    private readonly IWebSocketClient _socketClient;
    public TerrariumServiceWS(IWebSocketClient socketClient)
    {
        _socketClient = socketClient;
    }
    
    public void SendTerrariumLimits(TerrariumLimits terrariumLimits)
    {
        _socketClient.SendConfigurationAsync(ConvertTemperatureLimitsToHex(terrariumLimits));
    }
    
    private string ConvertTemperatureLimitsToHex(TerrariumLimits terrariumLimits)
    {
        double minTemp = terrariumLimits.TemperatureLimitMin * 10D; // Multiply by 10 to match the expected format

        string minTempHexa = minTemp.ToString("X4"); // Convert to hexadecimal string with 4 digits

        double maxTemp = terrariumLimits.TemperatureLimitMax * 10D;

        string maxTempHexa = maxTemp.ToString("X4");

        return minTempHexa + maxTempHexa;
    }

    public void PublishTerrariumLimitCreated(TerrariumLimits terrariumLimits)
    {
        SendTerrariumLimits(terrariumLimits);
    }
}