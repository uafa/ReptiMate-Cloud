using Model;

namespace ReptiMate_Cloud.Services;

public interface ITerrariumServiceRest
{
    Task CreateTerrariumLimitsAsync(TerrariumLimits terrariumLimits);
    Task<TerrariumLimits> GetTerrariumLimitsAsync();
    Task<TerrariumBoundaries> UpdateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries);
    Task<TerrariumBoundaries> GetTerrariumBoundariesAsync();
}