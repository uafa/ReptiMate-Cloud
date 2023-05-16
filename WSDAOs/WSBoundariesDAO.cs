using Microsoft.EntityFrameworkCore;
using Model;
using Repository;

namespace WSDAOs;

public class WSBoundariesDAO : IWSBoundariesDAO
{
    private readonly DatabaseContext context;

    public WSBoundariesDAO(DatabaseContext context)
    {
        this.context = context;
    }
    
    public async Task<TerrariumBoundaries> GetBoundariesAsync()
    {
        var boundaries = await context.TerrariumBoundaries!.FirstAsync();
        return boundaries;
    }
}