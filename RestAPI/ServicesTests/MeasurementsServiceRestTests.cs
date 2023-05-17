using Model;
using ReptiMate_Cloud.Services;
using RestDAOs;
using Moq;
using Xunit;

namespace ReptiMate_Cloud.ServicesTests;

public class MeasurementsServiceRestTests
{
    private readonly MeasurementsServiceRest _measurementsService;
    private readonly Mock<IRestMeasurementsDAO> _measurementsDaoMock;

    
    public MeasurementsServiceRestTests()
    {
        _measurementsDaoMock = new Mock<IRestMeasurementsDAO>();
        _measurementsService = new MeasurementsServiceRest(_measurementsDaoMock.Object);
    }
    
}