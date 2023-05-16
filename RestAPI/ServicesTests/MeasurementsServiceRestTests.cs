using Model;
using ReptiMate_Cloud.Services;
using RestDAOs;
using Moq;
using Xunit;


namespace ReptiMate_Cloud.Tests;


public class MeasurementsServiceRestTests
{
    private readonly MeasurementsServiceRest _measurementsService;
    private readonly Mock<IRestMeasurementsDAO> _measurementsDaoMock;
    private readonly Mock<IRestTerrariumDAO> _terrariumDaoMock;
    private readonly Mock<IRestNotificationDAO> _notificationDaoMock;

    public MeasurementsServiceRestTests()
    {
        _measurementsDaoMock = new Mock<IRestMeasurementsDAO>();
        _terrariumDaoMock = new Mock<IRestTerrariumDAO>();
        _notificationDaoMock = new Mock<IRestNotificationDAO>();
        _measurementsService = new MeasurementsServiceRest(
            _measurementsDaoMock.Object,
            _terrariumDaoMock.Object,
            _notificationDaoMock.Object
        );
    }

    [Fact]
    public async Task GetLatestMeasurementAsync_ValidMeasurement_ReturnsMeasurement()
    {
        
        var expectedMeasurement = new Measurements
        {
            Id = Guid.NewGuid(),
            DateTime = DateTime.UtcNow,
            Temperature = 25.5,
            Humidity = 60.2,
            Co2 = 800
        };

        _measurementsDaoMock.Setup(mock => mock.GetLatestMeasurementAsync())
            .ReturnsAsync(expectedMeasurement);

        var expectedBoundaries = new TerrariumBoundaries
        {
            Id = Guid.NewGuid(),
            TemperatureBoundaryMin = 20.0,
            TemperatureBoundaryMax = 30.0,
            HumidityBoundaryMin = 40.0,
            HumidityBoundaryMax = 70.0,
            CO2BoundaryMin = 500,
            CO2BoundaryMax = 1000
        };

        _terrariumDaoMock.Setup(mock => mock.GetTerrariumBoundariesAsync())
            .ReturnsAsync(expectedBoundaries);

        // Act
        var result = await _measurementsService.GetLatestMeasurementAsync();

        // Assert
        Assert.Equal(expectedMeasurement, result);
    }

    [Fact]
    public async Task GetLatestMeasurementAsync_NullMeasurement_ThrowsException()
    {
        
        _measurementsDaoMock.Setup(mock => mock.GetLatestMeasurementAsync())
            .ReturnsAsync((Measurements)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _measurementsService.GetLatestMeasurementAsync());
    }
    
}

    