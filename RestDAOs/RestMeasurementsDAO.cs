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

    public async Task<IList<Measurements>> GetAllMeasurementsAsync(DateTime dateFrom, DateTime dateTo)
    {
        //check if the request was send with "null" value for dateTime
        if (dateFrom.Equals(DateTime.MinValue) || dateTo.Equals(DateTime.MinValue))
        {
            var allMeasurements = await _context.Measurements.ToListAsync();
            return allMeasurements;
        }

        var measurements = await _context.Measurements.ToListAsync();
        var specifiedMeasurements = measurements
            .Where(m => m.DateTime.Ticks >= dateFrom.Ticks && m.DateTime.Ticks <= dateTo.Ticks)
            .ToList();
        

        return specifiedMeasurements;
    }
}