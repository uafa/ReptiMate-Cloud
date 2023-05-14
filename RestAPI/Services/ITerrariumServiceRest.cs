using Microsoft.AspNetCore.Mvc;
using Model;

namespace ReptiMate_Cloud.Services;

public interface ITerrariumServiceRest
{
    Task<TerrariumLimits> UpdateTerrariumLimitsAsync(TerrariumLimits terrariumLimits);
    Task<TerrariumLimits> GetTerrariumLimitsAsync();
    Task<TerrariumBoundaries> UpdateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries);
    Task<TerrariumBoundaries> GetTerrariumBoundariesAsync();
    Task<Terrarium> CreateTerrariumAsync(Terrarium terrarium);
}