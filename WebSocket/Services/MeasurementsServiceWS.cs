using Model;
using Repository.DAO;

namespace WebSocket.Services;

public class MeasurementsServiceWS : IMeasurementsServiceWS
{
    private readonly IMeasurementsDAO measurementsDao;

    public MeasurementsServiceWS(IMeasurementsDAO measurementsDao)
    {
        this.measurementsDao = measurementsDao;
    }

    public void SendMeasurements(string data)
    {
        var measurements = new Measurements();

        var now = DateTime.Now;
        measurements.Id = Guid.NewGuid();
        measurements.DateTime = now;
        measurements.Temperature = GetTemperature(data);
        measurements.Co2 = GetCO2(data);
        measurements.Humidity = GetHumidity(data);

        measurementsDao.CreateMeasurementsAsync(measurements);
    }



    private double GetTemperature(string data)
    {
        string tempHexa = data.Substring(0, 4);

        //remember, this is 10x
        int tempDec = int.Parse(tempHexa, System.Globalization.NumberStyles.HexNumber);

        return tempDec / 10D;
    }

    private double GetHumidity(string data)
    {
        string humidityHexa = data.Substring(4, 4);

        //remember, this is 10x
        int humidityDec = int.Parse(humidityHexa, System.Globalization.NumberStyles.HexNumber);
        return humidityDec;
    }

    private double GetCO2(string data)
    {
        string co2Hexa = data.Substring(8, 4);

        //remember, this is 10x
        int co2Dec = int.Parse(co2Hexa, System.Globalization.NumberStyles.HexNumber);

        return co2Dec;
    }
}