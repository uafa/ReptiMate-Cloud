using System.Data;
using Model;
using Repository.DAO;

namespace ReptiMate_Cloud.Services;

public class MeasurementsServiceRest : IMeasurementsServiceRest
{

    private IMeasurementsDAO measurementsDao;

    public MeasurementsServiceRest(IMeasurementsDAO measurementsDao)
    {
        this.measurementsDao = measurementsDao;
    }
    

    public async Task<Measurements> GetLatestMeasurement()
    {
        var measure = await measurementsDao.GetLatestMeasurementAsync();

        if (measure == null) throw new Exception("Not found");
        
        return measure;
    }
}