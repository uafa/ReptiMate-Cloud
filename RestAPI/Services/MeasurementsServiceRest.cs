using System.Data;
using Model;
using Repository.DAO;

namespace ReptiMate_Cloud.Services;

public class MeasurementsServiceRest : IMeasurementsServiceRest
{
    private IMeasurementsDAO measurementsDao;
    private ITerrariumDAO terrariumDao;
    private INotificationDAO notificationDao;

    public MeasurementsServiceRest(IMeasurementsDAO measurementsDao, ITerrariumDAO terrariumDao, INotificationDAO notificationDao)
    {
        this.measurementsDao = measurementsDao;
        this.terrariumDao = terrariumDao;
        this.notificationDao = notificationDao;
    }

    public async Task<Measurements> GetLatestMeasurement()
    {
        var measure = await measurementsDao.GetLatestMeasurementAsync();

        if (measure == null) throw new Exception("Not found");

        Task<TerrariumBoundaries> boundaries = terrariumDao.GetTerrariumBoundariesAsync();

        Console.WriteLine("here");
        HumidityBoundaryCheck(measure, boundaries);
        Console.WriteLine("or here");
        TemperatureBoundaryCheck(measure, boundaries);
        Co2BoundaryCheck(measure, boundaries);

        return measure;
    }

    private void Co2BoundaryCheck(Measurements measure, Task<TerrariumBoundaries> boundaries)
    {
        double co2BoundaryMin = boundaries.Result.CO2BoundaryMin;
        double co2BoundaryMax = boundaries.Result.CO2BoundaryMax;
        
        if (measure.Co2 < co2BoundaryMin)
        {
            Notification notification = new Notification
            {
                Message = $"The CO2 is lower than the set boundary! Current humidity: {measure.Co2}", 
                DateTime = DateTime.Now,
                Status = false
            };

            notificationDao.CreateNotificationAsync(notification);
        }
        else if (measure.Co2 > co2BoundaryMax)
        {
            Notification notification = new Notification
            {
                Message = $"The CO2 is higher than the set boundary! Current humidity: {measure.Co2}", 
                DateTime = DateTime.Now,
                Status = false
            };

            notificationDao.CreateNotificationAsync(notification);
        }
    }

    private void TemperatureBoundaryCheck(Measurements measure, Task<TerrariumBoundaries> boundaries)
    {
        
        double temperatureBoundaryMin = boundaries.Result.TemperatureBoundaryMin;
        double temperatureBoundaryMax = boundaries.Result.TemperatureBoundaryMax;
        
        if (measure.Temperature < temperatureBoundaryMin)
        {
            Notification notification = new Notification
            {
                Message = $"The temperature is lower than the set boundary! Current humidity: {measure.Temperature}", 
                DateTime = DateTime.Now,
                Status = false
            };

            notificationDao.CreateNotificationAsync(notification);
        }
        else if (measure.Temperature > temperatureBoundaryMax)
        {
            Notification notification = new Notification
            {
                Message = $"The temperature is higher than the set boundary! Current humidity: {measure.Temperature}", 
                DateTime = DateTime.Now,
                Status = false
            };

            notificationDao.CreateNotificationAsync(notification);
        }
    }

    private  void HumidityBoundaryCheck(Measurements measurements, Task<TerrariumBoundaries> boundaries)
    {
        Console.WriteLine("Humidity start");
        double humidityBoundaryMin = boundaries.Result.HumidityBoundaryMin;
        double humidityBoundaryMax = boundaries.Result.HumidityBoundaryMax;
        
        if (measurements.Humidity < humidityBoundaryMin)
        {
            Notification notification = new Notification
            {
                Message = $"The humidity is lower than the set boundary! Current humidity: {measurements.Humidity}", 
                DateTime = DateTime.Now,
                Status = false
            };

            notificationDao.CreateNotificationAsync(notification);
        }
        else if (measurements.Humidity > humidityBoundaryMax)
        {
            Console.WriteLine("It s upper");
            Notification notification = new Notification
            {
                Message = $"The humidity is higher than the set boundary! Current humidity: {measurements.Humidity}", 
                DateTime = DateTime.Now,
                Status = false
            };

            notificationDao.CreateNotificationAsync(notification);
        }
    }
}