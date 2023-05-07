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

    public async Task<TerrariumLimits> CreateTerrariumLimitsAsync(TerrariumLimits terrariumLimits)
    {
        EntityEntry<TerrariumLimits> newTerrariumLimits = await context.TerrariumLimits.AddAsync(terrariumLimits);
        await context.SaveChangesAsync();
        return newTerrariumLimits.Entity;
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

    public async Task<TerrariumBoundaries> CreateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries)
    {
        EntityEntry<TerrariumBoundaries> newTerrariumBoundaries =
            await context.TerrariumBoundaries.AddAsync(terrariumBoundaries);
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
}