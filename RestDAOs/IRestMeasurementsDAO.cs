using Model;

namespace RestDAOs;

public interface IRestMeasurementsDAO
{
    public Task<Measurements> GetLatestMeasurementAsync();
}