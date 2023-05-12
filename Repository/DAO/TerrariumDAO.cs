using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model;

namespace Repository.DAO;

public class TerrariumDAO : ITerrariumDAO
{
    private readonly DatabaseContext context;
    private ITerrariumDAOEventHandler eventHandler;

    public TerrariumDAO(DatabaseContext context, ITerrariumDAOEventHandler eventHandler)
    {
        this.context = context;
        this.eventHandler = eventHandler;
    }

    public async Task<TerrariumLimits> UpdateTerrariumLimitsAsync(TerrariumLimits terrariumLimits)
    {
        TerrariumLimits? limits = await context.TerrariumLimits.FirstOrDefaultAsync();
        limits!.TemperatureLimitMax = terrariumLimits.TemperatureLimitMax;
        limits!.TemperatureLimitMin = terrariumLimits.TemperatureLimitMin;

        context.TerrariumLimits.Update(limits);
        await context.SaveChangesAsync();
        eventHandler.PublishTerrariumLimitCreated(limits);
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

    public void PublishTerrariumLimitCreated(TerrariumLimits terrariumLimits)
    {
        
    }
}