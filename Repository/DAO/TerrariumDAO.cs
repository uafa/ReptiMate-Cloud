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

    public async Task<TerrariumLimits> CreateTerrariumLimitsAsync(TerrariumLimits terrariumLimits)
    {
        EntityEntry<TerrariumLimits> newLimits = await context.TerrariumLimits.AddAsync(terrariumLimits);
        await context.SaveChangesAsync();
        eventHandler.PublishTerrariumLimitCreated(newLimits.Entity);
        return newLimits.Entity;
    }
    

    public async Task<TerrariumLimits> GetTerrariumLimitsAsync()
    {
        TerrariumLimits? limits = await context.TerrariumLimits.LastAsync();

        if (limits == null)
            throw new Exception("No limits found");

        return limits;
    }

    public async Task<TerrariumBoundaries> CreateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries)
    {
        EntityEntry<TerrariumBoundaries> newTerrariumBoundaries = await context.TerrariumBoundaries.AddAsync(terrariumBoundaries);
        await context.SaveChangesAsync();
        return newTerrariumBoundaries.Entity;
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