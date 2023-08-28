using CodingExercise.Interfaces;
using CodingExercise.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace CodingExercise.Tests
{
    [TestClass]
    public class TransponderRepositoryFactoryTests
    {
        private Mock<ILogger<TransponderRepositoryFactory>> _mockLogger;
        private TransponderRepositoryFactory _transponderRepositoryFactory;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<TransponderRepositoryFactory>>();
            _transponderRepositoryFactory = new TransponderRepositoryFactory(_mockLogger.Object);
        }

        [TestMethod]
        public void GetRepository_ShouldReturnClassicRepository_ForOlderVehicles()
        {
            //Arrange
            var year = DateTime.Now.Year - 26;

            //Act
            var sut = _transponderRepositoryFactory.GetTransponderRepository(year);

            //Assert
            Assert.IsInstanceOfType(sut, typeof(ClassicTransponderRepository));
        }

        [TestMethod]
        public void GetRepository_ShouldReturnModernRepository_ForNewerVehicles()
        {
            //Arrange
            var year = DateTime.Now.Year;

            //Act
            var sut = _transponderRepositoryFactory.GetTransponderRepository(year);

            //Assert
            Assert.IsInstanceOfType(sut, typeof(ModernTransponderRepository));
        }
    }
}
