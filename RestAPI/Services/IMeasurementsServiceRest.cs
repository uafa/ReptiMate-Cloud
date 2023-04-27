using Model;

namespace ReptiMate_Cloud.Services;

public interface IMeasurementsServiceRest
{
    Task<Measurements> getLatestMeasurement();
}