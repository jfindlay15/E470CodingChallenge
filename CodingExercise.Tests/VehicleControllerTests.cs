using AutoMapper;
using CodingExercise.Controllers;
using CodingExercise.Dtos;
using CodingExercise.Interfaces;
using CodingExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CodingExercise.Tests
{
    [TestClass]
    public class VehicleControllerTests
    {
        private Mock<ILogger<VehicleController>> _mockLogger;
        private Mock<IMapper> _mockMapper;
        private Mock<IVehicleService> _vehicleService;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<VehicleController>>();
            _mockMapper = new Mock<IMapper>();
            _vehicleService = new Mock<IVehicleService>();
        }

        [TestMethod]
        public async Task ShouldReturnOkOnCreatedVehicle()
        {
            //Arrange 
            var vehicleDto = new VehicleDto { Make = "Dodge", Model = "Ram", Year = "2019" };
            var vehicle = new Vehicle { Id = 1, Make = "Dodge", Model = "Ram", Year = "2019" };
            _vehicleService.Setup(a => a.Create(It.IsAny<Vehicle>())).Returns(Task.FromResult(vehicle));
            var sut = new VehicleController(_mockLogger.Object, _vehicleService.Object, _mockMapper.Object);

            //Act
            var result = await sut.CreateVehicle(vehicleDto);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task CreateVehicle_ShouldReturnBadRequestOnNullInput()
        {
            //Arrange
            var sut = new VehicleController(_mockLogger.Object, _vehicleService.Object, _mockMapper.Object);

            //Act
            try
            {
                var result = await  sut.CreateVehicle(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.IsTrue(e.Message == "Value cannot be null. (Parameter 'vehicleDto')");
            }
        }

        [TestMethod]
        public async Task CreateVehicle_ShouldReturnBadRequestWhenModelIsInvalid()
        {
            //Arrange
            var vehicleDto = new VehicleDto { Make = "Dodge", Model = null, Year = "2019"};
            var sut = new VehicleController(_mockLogger.Object, _vehicleService.Object, _mockMapper.Object);
            sut.ModelState.AddModelError("Model", "Model is required");

            //Act
            try
            {
                var result = await sut.CreateVehicle(vehicleDto);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "Operation is not valid due to the current state of the object.");
            }
        }
    }
}
