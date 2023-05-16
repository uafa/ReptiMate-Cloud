using Model;

namespace RestDAOs;

public interface IRestTerrariumDAO
{
    public Task<TerrariumLimits> UpdateTerrariumLimitsAsync(TerrariumLimits terrariumLimits);
    public Task<TerrariumLimits> GetTerrariumLimitsAsync();
    public Task<TerrariumBoundaries> UpdateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries);
    public Task<TerrariumBoundaries> GetTerrariumBoundariesAsync();
    public Task CreateTerrariumAsync(Terrarium terrarium);
    public Task<Terrarium> GetTerrariumAsync();
}