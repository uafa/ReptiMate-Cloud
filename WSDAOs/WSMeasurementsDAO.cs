using Model;
using Repository;

namespace WSDAOs;

public class WSMeasurementsDAO : IWSMeasurementsDAO
{
    
    private readonly DatabaseContext _context;

    public WSMeasurementsDAO(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task CreateMeasurementsAsync(Measurements measurements)
    {
        await _context.Measurements.AddAsync(measurements);
        await _context.SaveChangesAsync();
    }
}