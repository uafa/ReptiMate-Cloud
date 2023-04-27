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
        return await measurementsDao.getLatestMeasurement();
    }
}