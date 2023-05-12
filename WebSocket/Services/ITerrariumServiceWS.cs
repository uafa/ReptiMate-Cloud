using Model;

namespace WebSocket.Services;

public interface ITerrariumServiceWS
{
    Task<TerrariumLimits> GetTerrariumLimitsAsync();
}