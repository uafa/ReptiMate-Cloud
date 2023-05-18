using Model;

namespace RestDAOs;

public interface IRestMeasurementsDAO
{
    public Task<Measurements> GetLatestMeasurementAsync();

    public Task<IList<Measurements>> GetAllMeasurementsAsync(DateTime dateFrom, DateTime dateTo);
}