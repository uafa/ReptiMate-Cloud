using Model;

namespace Repository.DAO;

public interface ITerrariumDAO
{
    public Task<TerrariumLimits> UpdateTerrariumLimitsAsync(TerrariumLimits terrariumLimits);
    public Task<TerrariumLimits> GetTerrariumLimitsAsync();
    public Task<TerrariumBoundaries> UpdateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries);
    public Task<TerrariumBoundaries> GetTerrariumBoundariesAsync();
}