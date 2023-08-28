using CodingExercise.Interfaces;
using CodingExercise.Models;
using CodingExercise.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CodingExercise.Tests
{
    [TestClass]
    public class TransponderServiceTests
    {
        private Mock<ITransponderRepository> _mockClassicTransponderRepository;
        private Mock<ITransponderRepository> _mockModernTransponderRepository;
        private Mock<ITransponderRepositoryFactory> _mockTransponderFactory;
        private Mock<ILogger<TransponderService>> _mockLogger;
        private TransponderService _transponderService;

        [TestInitialize]
        public void Setup()
        {
            _mockClassicTransponderRepository = new Mock<ITransponderRepository>();
            _mockModernTransponderRepository = new Mock<ITransponderRepository>();
            _mockTransponderFactory = new Mock<ITransponderRepositoryFactory>();
            _mockLogger = new Mock<ILogger<TransponderService>>();
            _transponderService = new TransponderService(_mockLogger.Object, _mockTransponderFactory.Object);
        }

        [TestMethod]
        public void ShouldCreateClassicTransponder_ForOlderVehicles()
        {
            //Arrange
            var vehicle = new Vehicle { Id = 1, Make = "Dodge", Model = "Ram", Year = (DateTime.Now.Year - 26).ToString() };
            _mockTransponderFactory.Setup(a => a.GetTransponderRepository(It.IsAny<int>()))
                .Returns(_mockClassicTransponderRepository.Object);

            //Act 
            _transponderService.Create(vehicle);

            //Assert
            _mockClassicTransponderRepository.Verify(repo => repo.Create(It.IsAny<Vehicle>()), Times.Once);
            _mockModernTransponderRepository.Verify(repo => repo.Create(It.IsAny<Vehicle>()), Times.Never);
        }

        [TestMethod]
        public void CreateTransponder_ShouldUserModernTransponderRepository_ForNewerVehicles()
        {
            //Arrange
            var vehicle = new Vehicle { Year = (DateTime.Now.Year).ToString() };
            _mockTransponderFactory.Setup(a => a.GetTransponderRepository(int.Parse(vehicle.Year)))
                .Returns(_mockModernTransponderRepository.Object);

            //Act 
            _transponderService.Create(vehicle);

            //Assert
            _mockModernTransponderRepository.Verify(repo => repo.Create(It.IsAny<Vehicle>()), Times.Once);
            _mockClassicTransponderRepository.Verify(repo => repo.Create(It.IsAny<Vehicle>()), Times.Never);
        }

        //Exception Handling

        [TestMethod]
        public void CreateTransponder_ShouldReturnNull_WhenExceptionThrown()
        {
            //Arrange
            _mockTransponderFactory.Setup(a => a.GetTransponderRepository(It.IsAny<int>()))
                .Throws(new Exception());

            //Act
            try
            {
                var sut = _transponderService.Create(new Vehicle());
            }
            catch (Exception e)
            {
                //This is caused from the Int.Parse on the year of the vehicle being null
                Assert.IsTrue(e.Message == "Value cannot be null. (Parameter 's')");
            }
        }

        [TestMethod]
        public void ShouldNotCreateTransponderForNullVehicle()
        {
            //Act
            var result = _transponderService.Create(null);

            //Assert 
            Assert.IsNull(result);
        }
    }
}
