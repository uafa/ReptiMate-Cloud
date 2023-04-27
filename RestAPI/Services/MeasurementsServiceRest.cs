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
    

    public async Task<Measurements> getLatestMeasurement()
    {
        var measure = await measurementsDao.getLatestMeasurement();

        if (measure == null) throw new Exception("Not found");
        
        return measure;
    }
}