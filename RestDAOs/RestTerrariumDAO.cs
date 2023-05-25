using Microsoft.EntityFrameworkCore;
using Model;
using Repository;

namespace RestDAOs;

public class RestTerrariumDAO : IRestTerrariumDAO
{
    private readonly DatabaseContext context;

    public RestTerrariumDAO(DatabaseContext context)
    {
        this.context = context;
    }

    public async Task<TerrariumLimits> UpdateTerrariumLimitsAsync(TerrariumLimits terrariumLimits)
    {
        TerrariumLimits? limits = await context.TerrariumLimits.FirstOrDefaultAsync();
        limits!.TemperatureLimitMax = terrariumLimits.TemperatureLimitMax;
        limits!.TemperatureLimitMin = terrariumLimits.TemperatureLimitMin;
        context.TerrariumLimits.Update(limits);
        await context.SaveChangesAsync();
        return limits;
    }

    public async Task<TerrariumLimits> GetTerrariumLimitsAsync()
    {
        TerrariumLimits? limits = await context.TerrariumLimits.FirstOrDefaultAsync();

        if (limits == null)
        {
            throw new Exception("No limits found");
        }

        return limits;
    }

    public async Task<TerrariumBoundaries> UpdateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries)
    {
        TerrariumBoundaries? boundaries = await context.TerrariumBoundaries.FirstOrDefaultAsync();
        boundaries!.HumidityBoundaryMax = terrariumBoundaries.HumidityBoundaryMax;
        boundaries.HumidityBoundaryMin = terrariumBoundaries.HumidityBoundaryMin;
        boundaries.TemperatureBoundaryMax = terrariumBoundaries.TemperatureBoundaryMax;
        boundaries.TemperatureBoundaryMin = terrariumBoundaries.TemperatureBoundaryMin;
        boundaries.CO2BoundaryMax = terrariumBoundaries.CO2BoundaryMax;
        boundaries.CO2BoundaryMin = terrariumBoundaries.CO2BoundaryMin;

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

    public async Task<Terrarium> GetTerrariumAsync()
    {
        Terrarium? terrarium = await context.Terrarium
            .Include(t => t.terrariumLimits)
            .Include(t => t.terrariumBoundaries)
            .FirstOrDefaultAsync();

        if (terrarium == null)
        {
            throw new Exception("No terrarium found");
        }

        return terrarium;
    }
}