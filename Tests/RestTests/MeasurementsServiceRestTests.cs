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
        //Arrange
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
    
    
    [Fact]
    public async Task GetAllMeasurementsAsync_NoMeasurementsFound_ThrowsException()
    {
        // Arrange
        DateTime dateFrom = new DateTime(2023, 1, 1);
        DateTime dateTo = new DateTime(2023, 12, 31);
    
        _measurementsDaoMock.Setup(mock => mock.GetAllMeasurementsAsync(dateFrom, dateTo))
            .ReturnsAsync((IList<Measurements>)null);
    
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _measurementsService.GetAllMeasurementsAsync(dateFrom, dateTo));
    }
    
    
    [Fact]
    public async Task GetAllMeasurementsAsync_ValidDates_ReturnsMeasurements()
    {
        // Arrange
        var dateFrom = new DateTime(2023, 1, 1);
        var dateTo = new DateTime(2023, 1, 31);

        // Create a list of measurements for the test scenario
        var measurements = new List<Measurements>
        {
            new Measurements { Id = Guid.NewGuid(), Temperature = 25.5, Humidity = 60.2, Co2 = 400, DateTime = new DateTime(2023, 1, 5) },
            new Measurements { Id = Guid.NewGuid(), Temperature = 26.3, Humidity = 61.7, Co2 = 420, DateTime = new DateTime(2023, 1, 10) },
            new Measurements { Id = Guid.NewGuid(), Temperature = 27.1, Humidity = 63.5, Co2 = 410, DateTime = new DateTime(2023, 1, 15) }
        };

        // Create a mock of the IRestMeasurementsDAO
        var measurementsDaoMock = new Mock<IRestMeasurementsDAO>();

        // Set up the mock behavior for GetAllMeasurementsAsync
        measurementsDaoMock.Setup(dao => dao.GetAllMeasurementsAsync(dateFrom, dateTo))
            .ReturnsAsync(measurements.Where(m => m.DateTime >= dateFrom && m.DateTime <= dateTo).ToList());

        var measurementsService = new MeasurementsServiceRest(measurementsDaoMock.Object);

        // Act
        var result = await measurementsService.GetAllMeasurementsAsync(dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count); // Assert the expected number of measurements
    }
}
    
    
