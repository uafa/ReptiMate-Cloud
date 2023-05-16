using Microsoft.EntityFrameworkCore;
using Model;
using Repository;

namespace WSDAOs;

public class WSTerrariumDAO : IWSTerrariumDAO
{
    private readonly DatabaseContext context;

    public WSTerrariumDAO(DatabaseContext context)
    {
        this.context = context;
    }

    public async Task<TerrariumLimits> GetTerrariumLimitsAsync()
    {
        TerrariumLimits? limits = await context.TerrariumLimits.FirstOrDefaultAsync();

        if (limits == null)
            throw new Exception("No limits found");

        return limits;
    }
}