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

        TerrariumBoundaries boundaries = await terrariumDao.GetTerrariumBoundariesAsync();

        await HumidityBoundaryCheckAsync(measure, boundaries);
        await TemperatureBoundaryCheckAsync(measure, boundaries);
        await Co2BoundaryCheckAsync(measure, boundaries);

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
    
    private async Task HumidityBoundaryCheckAsync(Measurements measurements, TerrariumBoundaries boundaries)
    {
        double humidityBoundaryMin = boundaries.HumidityBoundaryMin;
        double humidityBoundaryMax = boundaries.HumidityBoundaryMax;
        double diff;

        if (measurements.Humidity < humidityBoundaryMin)
        {
            diff = humidityBoundaryMin - measurements.Humidity;
            Notification notification = new Notification
            {
                Message = $"Humidity level is outside of the boundary. The current value is: {measurements.Humidity}," +
                          $" which is {diff} lower than the boundary that is: {humidityBoundaryMin}.", 
                DateTime = DateTime.UtcNow,
                Status = false
            };

           await notificationDao.CreateNotificationAsync(notification);
        }
        else if (measurements.Humidity > humidityBoundaryMax)
        {
            diff = measurements.Humidity - humidityBoundaryMax;
            Notification notification = new Notification
            {
                Message = $"Humidity level is outside of the boundary. The current value is: {measurements.Humidity}," +
                          $" which is {diff} higher than the boundary that is: {humidityBoundaryMax}.", 
                DateTime = DateTime.UtcNow,
                Status = false
            };

            await notificationDao.CreateNotificationAsync(notification);
        }
    }    
    
    private async Task  TemperatureBoundaryCheckAsync(Measurements measurements, TerrariumBoundaries boundaries) 
    {
        double temperatureBoundaryMin = boundaries.TemperatureBoundaryMin; 
        double temperatureBoundaryMax = boundaries.TemperatureBoundaryMax;
        double diff;
        
        if (measurements.Temperature < temperatureBoundaryMin) 
        { 
            diff = temperatureBoundaryMin - measurements.Temperature;
            Notification notification = new Notification
            {
                Message = $"Temperature level is outside of the boundary. The current value is: {measurements.Temperature}," +
                          $" which is {diff} lower than the boundary that is: {temperatureBoundaryMin}.",  
                DateTime = DateTime.UtcNow,
                Status = false
            };
     
            await notificationDao.CreateNotificationAsync(notification);
        }
        else if (measurements.Temperature > temperatureBoundaryMax)
        {
            Console.WriteLine(measurements.Temperature);
            Console.WriteLine(temperatureBoundaryMax);
            diff = measurements.Temperature - temperatureBoundaryMax;
            Notification notification = new Notification
            {
                Message = $"Temperature level is outside of the boundary. The current value is: {measurements.Temperature}," +
                          $" which is {diff} higher than the boundary that is: {temperatureBoundaryMax}.",  
                DateTime = DateTime.UtcNow,
                Status = false
            };
     
            await notificationDao.CreateNotificationAsync(notification);
        }
    }
    
    private async Task Co2BoundaryCheckAsync(Measurements measurements, TerrariumBoundaries boundaries)
        {
            double co2BoundaryMin = boundaries.CO2BoundaryMin;
            double co2BoundaryMax = boundaries.CO2BoundaryMax;
            double diff;
            
            if (measurements.Co2 < co2BoundaryMin)
            {
                diff = co2BoundaryMin - measurements.Co2;
                Notification notification = new Notification
                {
                    Message = $"Temperature level is outside of the boundary. The current value is: {measurements.Co2}," +
                              $" which is {diff} lower than the boundary that is: {co2BoundaryMin}.",  
                    DateTime = DateTime.UtcNow,
                    Status = false
                };
    
                await notificationDao.CreateNotificationAsync(notification);
            }
            else if (measurements.Co2 > co2BoundaryMax)
            {
                diff = measurements.Co2 - co2BoundaryMax;
                Notification notification = new Notification
                {
                    Message = $"Temperature level is outside of the boundary. The current value is: {measurements.Co2}," +
                              $" which is {diff} higher than the boundary that is: {co2BoundaryMax}.",
                    DateTime = DateTime.UtcNow,
                    Status = false
                };
    
                await notificationDao.CreateNotificationAsync(notification);
            }
        }
}