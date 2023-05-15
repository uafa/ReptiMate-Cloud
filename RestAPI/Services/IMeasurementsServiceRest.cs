using Model;

namespace ReptiMate_Cloud.Services;

public interface IMeasurementsServiceRest
{
    Task<Measurements> GetLatestMeasurementAsync();
}