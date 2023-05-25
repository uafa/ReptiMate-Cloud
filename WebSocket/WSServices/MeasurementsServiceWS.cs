using Model;
using WSDAOs;

namespace WebSocket.Services;

public class MeasurementsServiceWS : IMeasurementsServiceWS
{
    private readonly IWSMeasurementsDAO measurementsDao;
    private readonly IWSNotificationDAO notificationDao;
    private readonly IWSBoundariesDAO boundariesDao;
    public MeasurementsServiceWS(IWSMeasurementsDAO measurementsDao, IWSNotificationDAO notificationDao, IWSBoundariesDAO boundariesDao)
    {
        this.measurementsDao = measurementsDao;
        this.notificationDao = notificationDao;
        this.boundariesDao = boundariesDao;
    }

    public async Task ReceiveMeasurementsAsync(string data)
    {
        var measurements = new Measurements();

        var now = DateTime.UtcNow;
        measurements.Id = Guid.NewGuid();
        measurements.DateTime = now;
        measurements.Temperature = GetTemperature(data);
        measurements.Co2 = GetCO2(data);
        measurements.Humidity = GetHumidity(data);

        await measurementsDao.ReceiveMeasurementsAsync(measurements);

        var boundaries = await boundariesDao.GetBoundariesAsync();

        await BoundaryCheckAsync("Temperature", measurements.Temperature, boundaries.TemperatureBoundaryMin,
            boundaries.TemperatureBoundaryMax);
        await BoundaryCheckAsync("Humidity", measurements.Humidity, boundaries.HumidityBoundaryMin,
            boundaries.HumidityBoundaryMax);
        await BoundaryCheckAsync("CO2", measurements.Co2, boundaries.CO2BoundaryMin,
            boundaries.CO2BoundaryMax);
    }


    public double GetTemperature(string data)
    {
        string tempHexa = data.Substring(0, 4);

        //remember, this is 10x
        int tempDec = int.Parse(tempHexa, System.Globalization.NumberStyles.HexNumber);

        return tempDec / 10D;
    }

    public double GetHumidity(string data)
    {
        string humidityHexa = data.Substring(4, 4);

        //remember, this is 10x
        int humidityDec = int.Parse(humidityHexa, System.Globalization.NumberStyles.HexNumber);
        return humidityDec / 10D;
    }

    public double GetCO2(string data)
    {
        string co2Hexa = data.Substring(8, 4);

        //remember, this is 10x
        int co2Dec = int.Parse(co2Hexa, System.Globalization.NumberStyles.HexNumber);

        return co2Dec;
    }


    public async Task BoundaryCheckAsync(string text, double value, double min, double max)
    {
        string? message = null;

        if (value < min)
        {
            double diff = min - value;
            message = $"{text} level is outside of the boundary. The current value is: {value}," +
                      $" which is {diff} lower than the boundary that is: {min}.";
        }
        else if (value > max)
        {
            double diff = value - max;
            message = $"{text} level is outside of the boundary. The current value is: {value}," +
                      $" which is {diff} higher than the boundary that is: {max}.";
        }

        if (message != null)
        {
            Notification notification = new Notification
            {
                Message = message, 
                DateTime = DateTime.UtcNow,
                Status = false
            };

            await notificationDao.CreateNotificationAsync(notification);
        }
    }
}