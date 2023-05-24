using Model;

namespace WSDAOs;

public interface IWSMeasurementsDAO
{
    public Task ReceiveMeasurementsAsync(Measurements measurements);
}