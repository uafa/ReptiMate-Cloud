using Model;

namespace WebSocket.Services;

public interface ITerrariumServiceWS
{
    public void SendTerrariumLimits(TerrariumLimits terrariumLimits);
}