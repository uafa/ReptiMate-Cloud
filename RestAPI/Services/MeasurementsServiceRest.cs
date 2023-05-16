using Model;
using RestDAOs;

namespace ReptiMate_Cloud.Services;

public class MeasurementsServiceRest : IMeasurementsServiceRest
{
    private IRestMeasurementsDAO measurementsDao;
    private IRestTerrariumDAO terrariumDao;
    private IRestNotificationDAO notificationDao;

    public MeasurementsServiceRest(IRestMeasurementsDAO measurementsDao, IRestTerrariumDAO terrariumDao, IRestNotificationDAO notificationDao)
    {
        this.measurementsDao = measurementsDao;
        this.terrariumDao = terrariumDao;
        this.notificationDao = notificationDao;
    }

    public async Task<Measurements> GetLatestMeasurementAsync()
    {
        var measure = await measurementsDao.GetLatestMeasurementAsync();

        if (measure == null) throw new Exception("Not found");

        return measure;
    }

}