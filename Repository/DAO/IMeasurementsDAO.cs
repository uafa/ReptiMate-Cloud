using Model;

namespace Repository.DAO;

public interface IMeasurementsDAO
{
    public Task<Measurements> getLatestMeasurement();
}