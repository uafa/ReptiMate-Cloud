using Microsoft.EntityFrameworkCore;
using Model;
using Repository;

namespace RestDAOs;

public class RestMeasurementsDAO : IRestMeasurementsDAO
{
    private readonly DatabaseContext _context;

    public RestMeasurementsDAO(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Measurements> GetLatestMeasurementAsync()
    {
        Measurements? latestMeasurement = await _context.Measurements
            .OrderByDescending(m => m.DateTime)
            .FirstOrDefaultAsync();

        if (latestMeasurement == null)
            throw new Exception("No measurements found");
            
        return latestMeasurement;
    }
}