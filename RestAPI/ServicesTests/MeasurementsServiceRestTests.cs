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

    
    public MeasurementsServiceRestTests()
    {
        _measurementsDaoMock = new Mock<IRestMeasurementsDAO>();
        _measurementsService = new MeasurementsServiceRest(_measurementsDaoMock.Object);
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
        
        // Act
        var result = await _measurementsService.GetLatestMeasurementAsync();
    
        // Assert
        Assert.Equal(expectedMeasurement, result);
    }
    
    [Fact]
    public async Task GetLatestMeasurementAsync_ValidMultipleMeasurements_ReturnsMeasurement()
    {
        // Arrange
        var measurements = new List<Measurements>
        {
            new Measurements { Id = Guid.NewGuid(), DateTime = new DateTime(2023, 1, 5), Temperature = 25.5, Humidity = 60.2, Co2 = 800 },
            new Measurements { Id = Guid.NewGuid(), DateTime = new DateTime(2023, 1, 10), Temperature = 26.3, Humidity = 61.7, Co2 = 820 },
            new Measurements { Id = Guid.NewGuid(), DateTime = new DateTime(2023, 1, 15), Temperature = 27.1, Humidity = 63.5, Co2 = 810 }
        };

        var latestMeasurement = measurements.OrderByDescending(m => m.DateTime).First();

        var measurementsDaoMock = new Mock<IRestMeasurementsDAO>();
        measurementsDaoMock.Setup(mock => mock.GetLatestMeasurementAsync()).ReturnsAsync(latestMeasurement);

        var measurementsService = new MeasurementsServiceRest(measurementsDaoMock.Object);

        // Act
        var result = await measurementsService.GetLatestMeasurementAsync();

        // Assert
        Assert.Equal(latestMeasurement, result);
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
    
    
