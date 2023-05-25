using Microsoft.AspNetCore.Mvc;
using Model;
using RestDAOs;

namespace ReptiMate_Cloud.Services;

public class TerrariumServiceRest : ITerrariumServiceRest
{
    private IRestTerrariumDAO terrariumDao;

    public TerrariumServiceRest(IRestTerrariumDAO terrariumDao)
    {
        this.terrariumDao = terrariumDao;
    }

    public async Task<TerrariumLimits> UpdateTerrariumLimitsAsync(TerrariumLimits terrariumLimits)
    {
        return await terrariumDao.UpdateTerrariumLimitsAsync(terrariumLimits);
    }

    public async Task<TerrariumLimits> GetTerrariumLimitsAsync()
    {
        var limits = await terrariumDao.GetTerrariumLimitsAsync();
        if (limits == null)
        {
            throw new Exception("Not found");
        }
        return limits;
    }

    public async Task<TerrariumBoundaries> UpdateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries)
    {
        return await terrariumDao.UpdateTerrariumBoundariesAsync(terrariumBoundaries);
    }

    public async Task<TerrariumBoundaries> GetTerrariumBoundariesAsync()
    {
        var boundaries = await terrariumDao.GetTerrariumBoundariesAsync();

        if (boundaries == null) throw new Exception("Not found");

        return boundaries;
    }

    public async Task<Terrarium> GetTerrariumAsync()
    {
        var terrarium = await terrariumDao.GetTerrariumAsync();

        if (terrarium == null) throw new Exception("Not found");
        return terrarium;
    }
    
}