using Model;
using RestDAOs;

namespace ReptiMate_Cloud.Services;

public class MeasurementsServiceRest : IMeasurementsServiceRest
{
    private IRestMeasurementsDAO measurementsDao;
    private IRestTerrariumDAO terrariumDao;

    public MeasurementsServiceRest(IRestMeasurementsDAO measurementsDao)
    {
        this.measurementsDao = measurementsDao;
    }

    public async Task<Measurements> GetLatestMeasurementAsync()
    {
        var measure = await measurementsDao.GetLatestMeasurementAsync();

        if (measure == null) throw new Exception("Not found");

        return measure;
    }
    
    public async Task<ICollection<Measurements>> GetAllMeasurementsAsync(DateTime dateFrom, DateTime dateTo)
    {
        var measurements = await measurementsDao.GetAllMeasurementsAsync(dateFrom, dateTo);
        if (measurements == null)
        {
            throw new Exception("No measurements found");
        }
        return measurements;
    }
}