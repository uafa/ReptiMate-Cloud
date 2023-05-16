using Model;

namespace ReptiMate_Cloud.Services;

public interface IMeasurementsServiceRest
{
    Task<Measurements> GetLatestMeasurementAsync();

    Task<ICollection<Measurements>> GetAllMeasurementsAsync(DateTime dateFrom, DateTime dateTo);
}