using Model;
using Repository.DAO;
using WebSocket.Gateway;

namespace WebSocket.Services;

public class TerrariumServiceWS : ITerrariumServiceWS
{
    private readonly ITerrariumDAO terrariumDao;
    public TerrariumServiceWS(ITerrariumDAO terrariumDao)
    {
        this.terrariumDao = terrariumDao;
    }

    public async Task<TerrariumLimits> GetTerrariumLimitsAsync()
    {
        return await terrariumDao.GetTerrariumLimitsAsync();
    }
    
}