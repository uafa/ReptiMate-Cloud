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

    public async Task<TerrariumBoundaries> CreateTerrariumBoundariesAsync(TerrariumBoundaries terrariumBoundaries)
    {
        EntityEntry<TerrariumBoundaries> newTerrariumBoundaries = await context.Boundaries.AddAsync(terrariumBoundaries);
        await context.SaveChangesAsync();
        return newTerrariumBoundaries.Entity;
    }

    public async Task<TerrariumBoundaries> GetTerrariumBoundariesAsync()
    {
        TerrariumBoundaries? boundaries = await context.Boundaries.FirstOrDefaultAsync();

        if (boundaries == null)
        {
            throw new Exception("No boundaries found");
        }

        return boundaries;
    }
}