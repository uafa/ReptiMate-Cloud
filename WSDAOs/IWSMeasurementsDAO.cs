using Model;

namespace WSDAOs;

public interface IWSMeasurementsDAO
{
    public Task CreateMeasurementsAsync(Measurements measurements);
}