using Model;
using Repository.DAO;

namespace ReptiMate_Cloud.Services;

public class TerrariumServiceRest : ITerrariumServiceRest
{
    private ITerrariumDAO terrariumDao;

    public TerrariumServiceRest(ITerrariumDAO terrariumDao)
    {
        this.terrariumDao = terrariumDao;
    }
    
    public Task CreateTerrariumLimitsAsync(TerrariumLimits terrariumLimits)
    {
        throw new NotImplementedException();
    }

    public Task<TerrariumLimits> GetTerrariumLimitsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TerrariumBoundaries> CreateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries)
    {
        return await terrariumDao.CreateTerrariumBoundariesAsync(terrariumBoundaries);
    }

    public async Task<TerrariumBoundaries> GetTerrariumBoundariesAsync()
    {
        var boundaries = await terrariumDao.GetTerrariumBoundariesAsync();

        if (boundaries == null) throw new Exception("Not found");

        return boundaries;
    }
}