using Model;

namespace Repository.DAO;

public interface ITerrariumDAO
{
    public Task<TerrariumLimits> CreateTerrariumLimitsAsync(TerrariumLimits terrariumLimits);
    public Task<TerrariumLimits> GetTerrariumLimitsAsync();
    public Task<TerrariumBoundaries> CreateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries);
    public Task<TerrariumBoundaries> GetTerrariumBoundariesAsync();
}