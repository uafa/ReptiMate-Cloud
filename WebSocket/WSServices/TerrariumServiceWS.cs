using Model;
using WebSocket.Gateway;
using WSDAOs;

namespace WebSocket.Services;

public class TerrariumServiceWS : ITerrariumServiceWS
{
    private readonly IWSTerrariumDAO terrariumDao;
    public TerrariumServiceWS(IWSTerrariumDAO terrariumDao)
    {
        this.terrariumDao = terrariumDao;
    }

    public async Task<TerrariumLimits> GetTerrariumLimitsAsync()
    {
        return await terrariumDao.GetTerrariumLimitsAsync();
    }
    
}