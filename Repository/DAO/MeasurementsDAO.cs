using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository.DAO;

public class MeasurementsDAO : IMeasurementsDAO
{
    private readonly DatabaseContext _context;

    public MeasurementsDAO(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Measurements> GetLatestMeasurementAsync()
    {
        Measurements? latestMeasurement = await _context.Measurements.OrderByDescending(m => m.Date)
            .ThenByDescending(m => m.Time).FirstOrDefaultAsync();

        if (latestMeasurement == null)
            throw new Exception("No measurements found");
        return latestMeasurement;
    }

    public async Task CreateMeasurementsAsync(Measurements measurements)
    {
        await _context.Measurements.AddAsync(measurements);
        await _context.SaveChangesAsync();
    }
}