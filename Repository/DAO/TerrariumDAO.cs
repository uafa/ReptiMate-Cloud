using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model;

namespace Repository.DAO;

public class TerrariumDAO : ITerrariumDAO
{
    private readonly DatabaseContext context;

    public TerrariumDAO(DatabaseContext context)
    {
        this.context = context;
    }

    public Task CreateTerrariumLimitsAsync(TerrariumLimits terrariumLimits)
    {
        throw new NotImplementedException();
    }

    public Task<TerrariumLimits> GetTerrariumLimitsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TerrariumBoundaries> UpdateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries)
    {
        TerrariumBoundaries? boundaries = await context.TerrariumBoundaries.FirstOrDefaultAsync();
        boundaries!.HumidityBoundaryMax = terrariumBoundaries.HumidityBoundaryMax;
        boundaries.HumidityBoundaryMin = terrariumBoundaries.HumidityBoundaryMin;
        boundaries.TemperatureBoundaryMax = terrariumBoundaries.TemperatureBoundaryMax;
        boundaries.TemperatureBoundaryMin = terrariumBoundaries.TemperatureBoundaryMin;
        boundaries.HumidityBoundaryMax = terrariumBoundaries.HumidityBoundaryMax;
        boundaries.HumidityBoundaryMin = terrariumBoundaries.HumidityBoundaryMin;
        
        context.TerrariumBoundaries.Update(boundaries);
        await context.SaveChangesAsync();
        return boundaries;
    }

    public async Task<TerrariumBoundaries> GetTerrariumBoundariesAsync()
    {
        TerrariumBoundaries? boundaries = await context.TerrariumBoundaries.FirstOrDefaultAsync();

        if (boundaries == null)
        {
            throw new Exception("No boundaries found");
        }

        return boundaries;
    }
}