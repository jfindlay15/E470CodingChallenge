using CodingExercise.Interfaces;
using CodingExercise.Models;
using CodingExercise.Services;
using Moq;

namespace CodingExercise.Tests
{
    [TestClass]
    public class VehicleServiceTests
    {
        private Mock<IVehicleRepository> _mockVehicleRepository;
        private VehicleService _vehicleService;
        private Mock<Microsoft.Extensions.Logging.ILogger<VehicleService>> _mockLogger;
        private Mock<ITransponderService> _mockTransponderService;

        [TestInitialize]
        public void Setup()
        {
            _mockVehicleRepository = new Mock<IVehicleRepository>();
            _mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<VehicleService>>();
            _mockTransponderService = new Mock<ITransponderService>();
            _vehicleService = new VehicleService(_mockLogger.Object, _mockVehicleRepository.Object, _mockTransponderService.Object);
        }

        [TestMethod]
        public async Task ShouldInvokeVehicleCreatedEvent()
        {
            //Arrange
            var eventRaised = false;
            _vehicleService.VehicleCreated += (sender, e) => eventRaised = true;
            var vehicle = new Vehicle { Id = 1, Make = "Dodge", Model = "Ram", Year = "2019" };

            _mockVehicleRepository.Setup(x => x.Create(It.IsAny<Vehicle>())).Returns(Task.FromResult(vehicle));

            //Act
            await _vehicleService.Create(vehicle);

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void ShouldNotInvokeVehicleCreatedEventOnNullInput()
        {
            // Arrange
            var isEventTriggered = false;

            _vehicleService.VehicleCreated += (sender, e) => isEventTriggered = true;

            // Act
            _vehicleService.Create(null);

            // Assert
            Assert.IsFalse(isEventTriggered);
        }
    }
}
