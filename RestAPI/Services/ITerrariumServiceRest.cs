using Microsoft.AspNetCore.Mvc;
using Model;

namespace ReptiMate_Cloud.Services;

public interface ITerrariumServiceRest
{
    Task<TerrariumLimits> CreateTerrariumLimitsAsync(TerrariumLimits terrariumLimits);
    Task<TerrariumLimits> GetTerrariumLimitsAsync();
    Task<TerrariumBoundaries> CreateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries);
    Task<TerrariumBoundaries> GetTerrariumBoundariesAsync();
}