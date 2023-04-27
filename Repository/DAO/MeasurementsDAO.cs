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

    public async Task<Measurements> getLatestMeasurement()
    {
        Measurements? latestMeasurement = await _context.Measurements.OrderByDescending(m => m.Date).ThenByDescending(m=> m.Time).FirstOrDefaultAsync();
        if (latestMeasurement == null)
           throw new Exception("No measurements found");
        return latestMeasurement;
    }
}