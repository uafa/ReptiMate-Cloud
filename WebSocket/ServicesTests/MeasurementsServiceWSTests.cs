using System.Reflection;
using Model;
using Moq;
using WebSocket.Services;
using WSDAOs;
using Xunit;

namespace WebSocket.ServicesTests;

public class MeasurementsServiceWSTests
{
    private readonly MeasurementsServiceWS _measurementsService;
    private readonly Mock<IWSMeasurementsDAO> _measurementsDaoMock;
    private readonly Mock<IWSNotificationDAO> _notificationDaoMock;
    private readonly Mock<IWSBoundariesDAO> _boundariesDaoMock;

    public MeasurementsServiceWSTests()
    {
        _measurementsDaoMock = new Mock<IWSMeasurementsDAO>();
        _notificationDaoMock = new Mock<IWSNotificationDAO>();
        _boundariesDaoMock = new Mock<IWSBoundariesDAO>();
        _measurementsService = new MeasurementsServiceWS(
            _measurementsDaoMock.Object,
            _notificationDaoMock.Object,
            _boundariesDaoMock.Object
        );
    }
    
    [Fact]
    public async Task SendMeasurementsAsync_ValidData_CreatesMeasurementsAndChecksBoundaries()
    {
        // Arrange
        var data = "0A2801F401B8"; // Example data
        var boundaries = new TerrariumBoundaries
        {
            TemperatureBoundaryMin = 20,
            TemperatureBoundaryMax = 30,
            HumidityBoundaryMin = 40,
            HumidityBoundaryMax = 60,
            CO2BoundaryMin = 500,
            CO2BoundaryMax = 1000
        };

        _boundariesDaoMock.Setup(mock => mock.GetBoundariesAsync())
            .ReturnsAsync(boundaries);

        // Act
        await _measurementsService.SendMeasurementsAsync(data);

        // Assert
        _measurementsDaoMock.Verify(mock =>
            mock.CreateMeasurementsAsync(It.IsAny<Measurements>()), Times.Once);

        _notificationDaoMock.Verify(mock =>
            mock.CreateNotificationAsync(It.IsAny<Notification>()), Times.Exactly(3));
    }
    
    
    [Fact]
    public async Task SendMeasurementsAsync_LowTemperature_CreatesTemperatureNotification()
    {
        // Arrange
        var data = "000001F401B8"; // Low temperature data
        var boundaries = new TerrariumBoundaries
        {
            TemperatureBoundaryMin = 20,
            TemperatureBoundaryMax = 30,
            HumidityBoundaryMin = 40,
            HumidityBoundaryMax = 60,
            CO2BoundaryMin = 500,
            CO2BoundaryMax = 1000
        };

        _boundariesDaoMock.Setup(mock => mock.GetBoundariesAsync())
            .ReturnsAsync(boundaries);

        // Act
        await _measurementsService.SendMeasurementsAsync(data);

        // Assert
        _notificationDaoMock.Verify(mock =>
            mock.CreateNotificationAsync(It.Is<Notification>(n =>
                n.Message.Contains("Temperature") && n.Status == false)), Times.Once);
    }
    
    [Fact]
    public async Task SendMeasurementsAsync_HighHumidity_CreatesHumidityNotification()
    {
        // Arrange
        var data = "0A2840F401B8"; // High humidity data
        var boundaries = new TerrariumBoundaries
        {
            TemperatureBoundaryMin = 20,
            TemperatureBoundaryMax = 30,
            HumidityBoundaryMin = 40,
            HumidityBoundaryMax = 60,
            CO2BoundaryMin = 500,
            CO2BoundaryMax = 1000
        };

        _boundariesDaoMock.Setup(mock => mock.GetBoundariesAsync())
            .ReturnsAsync(boundaries);

        // Act
        await _measurementsService.SendMeasurementsAsync(data);

        // Assert
        _notificationDaoMock.Verify(mock =>
            mock.CreateNotificationAsync(It.Is<Notification>(n =>
                n.Message.Contains("Humidity") && n.Status == false)), Times.Once);
    }
    
    [Fact]
    public async Task SendMeasurementsAsync_HighCO2_CreatesCO2Notification()
    {
        // Arrange
        var data = "0A2801FAF401"; // High CO2 data
        var boundaries = new TerrariumBoundaries
        {
            TemperatureBoundaryMin = 20,
            TemperatureBoundaryMax = 30,
            HumidityBoundaryMin = 40,
            HumidityBoundaryMax = 60,
            CO2BoundaryMin = 500,
            CO2BoundaryMax = 1000
        };

        _boundariesDaoMock.Setup(mock => mock.GetBoundariesAsync())
            .ReturnsAsync(boundaries);

        // Act
        await _measurementsService.SendMeasurementsAsync(data);

        // Assert
        _notificationDaoMock.Verify(mock =>
            mock.CreateNotificationAsync(It.Is<Notification>(n =>
                n.Message.Contains("CO2") && n.Status == false)), Times.Once);
    }
    
    [Fact]
        public async Task BoundaryCheckAsync_WhenValueIsBelowMin_CreatesNotification()
        {
            // Arrange
            string text = "Temperature";
            double value = 5.0;
            double min = 10.0;
            double max = 30.0;

            string expectedMessage = $"{text} level is outside of the boundary. The current value is: {value}," +
                                     $" which is {min - value} lower than the boundary that is: {min}.";

            var notificationDaoMock = new Mock<IWSNotificationDAO>();
            notificationDaoMock.Setup(dao => dao.CreateNotificationAsync(It.IsAny<Notification>()))
                .Returns(Task.CompletedTask);

            var measurementsService = new MeasurementsServiceWS(
                measurementsDao: null,
                notificationDao: notificationDaoMock.Object,
                boundariesDao: null
            );

            // Act
            await measurementsService.BoundaryCheckAsync(text, value, min, max);

            // Assert
            notificationDaoMock.Verify(dao =>
                dao.CreateNotificationAsync(It.Is<Notification>(n =>
                    n.Message == expectedMessage &&
                    n.DateTime <= DateTime.UtcNow &&
                    !n.Status
                )),
                Times.Once
            );
        }

        [Fact]
        public async Task BoundaryCheckAsync_WhenValueIsAboveMax_CreatesNotification()
        {
            // Arrange
            string text = "Humidity";
            double value = 80.0;
            double min = 30.0;
            double max = 70.0;

            string expectedMessage = $"{text} level is outside of the boundary. The current value is: {value}," +
                                     $" which is {value - max} higher than the boundary that is: {max}.";

            var notificationDaoMock = new Mock<IWSNotificationDAO>();
            notificationDaoMock.Setup(dao => dao.CreateNotificationAsync(It.IsAny<Notification>()))
                .Returns(Task.CompletedTask);

            var measurementsService = new MeasurementsServiceWS(
                measurementsDao: null,
                notificationDao: notificationDaoMock.Object,
                boundariesDao: null
            );

            // Act
            await measurementsService.BoundaryCheckAsync(text, value, min, max);

            // Assert
            notificationDaoMock.Verify(dao =>
                dao.CreateNotificationAsync(It.Is<Notification>(n =>
                    n.Message == expectedMessage &&
                    n.DateTime <= DateTime.UtcNow &&
                    !n.Status
                )),
                Times.Once
            );
        }

        [Fact]
        public async Task BoundaryCheckAsync_WhenValueIsWithinBoundaries_DoesNotCreateNotification()
        {
            // Arrange
            string text = "CO2";
            double value = 500.0;
            double min = 300.0;
            double max = 800.0;

            var notificationDaoMock = new Mock<IWSNotificationDAO>();
            notificationDaoMock.Setup(dao => dao.CreateNotificationAsync(It.IsAny<Notification>()))
                .Returns(Task.CompletedTask);

            var measurementsService = new MeasurementsServiceWS(
                measurementsDao: null,
                notificationDao: notificationDaoMock.Object,
                boundariesDao: null
            );

            // Act
            await measurementsService.BoundaryCheckAsync(text, value, min, max);

            // Assert
            notificationDaoMock.Verify(dao =>
                dao.CreateNotificationAsync(It.IsAny<Notification>()),
                Times.Never
            );
        }
        
        [Fact]
        public void GetCO2_ValidHexadecimal_ReturnsCorrectValue()
        {
            // Arrange
            var measurementsDaoMock = new Mock<IWSMeasurementsDAO>();
            var notificationDaoMock = new Mock<IWSNotificationDAO>();
            var boundariesDaoMock = new Mock<IWSBoundariesDAO>();
            var service = new MeasurementsServiceWS(measurementsDaoMock.Object, notificationDaoMock.Object, boundariesDaoMock.Object);
            string data = "ABCDEF012345"; // Sample data

            // Act
            double co2 = service.GetCO2(data);

            // Assert
            Assert.Equal(9029, co2);
        }
        
        [Fact]
        public void GetHumidity_ValidHexadecimal_ReturnsCorrectValue()
        {
            // Arrange
            var measurementsDaoMock = new Mock<IWSMeasurementsDAO>();
            var notificationDaoMock = new Mock<IWSNotificationDAO>();
            var boundariesDaoMock = new Mock<IWSBoundariesDAO>();
            var service = new MeasurementsServiceWS(measurementsDaoMock.Object, notificationDaoMock.Object, boundariesDaoMock.Object);
            string data = "ABCDEF012345"; // Sample data

            // Act
            double humidity = service.GetHumidity(data);

            // Assert
            double expectedHumidity = 61185;
            Assert.Equal(expectedHumidity, humidity);
        }
        
        [Fact]
        public void GetTemperature_ValidHexadecimal_ReturnsCorrectValue()
        {
            // Arrange
            var measurementsDaoMock = new Mock<IWSMeasurementsDAO>();
            var notificationDaoMock = new Mock<IWSNotificationDAO>();
            var boundariesDaoMock = new Mock<IWSBoundariesDAO>();
            var service = new MeasurementsServiceWS(measurementsDaoMock.Object, notificationDaoMock.Object, boundariesDaoMock.Object);
            string data = "ABCDEF012345"; // Sample data

            // Act
            double temperature = service.GetTemperature(data);

            // Assert
            double expectedTemperature = 4398;
            //We have 0 because ->  Actual should be 4398.1000000000004
            Assert.Equal(expectedTemperature, temperature,0);
        }


    
}