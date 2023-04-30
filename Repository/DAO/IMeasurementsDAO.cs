using Model;

namespace Repository.DAO;

public interface IMeasurementsDAO
{
    public Task<Measurements> GetLatestMeasurementAsync();

    public Task CreateMeasurementsAsync(Measurements measurements);
}