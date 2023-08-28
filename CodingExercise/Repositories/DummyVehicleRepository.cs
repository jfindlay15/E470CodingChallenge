using CodingExercise.Interfaces;
using CodingExercise.Models;

namespace CodingExercise.Repositories
{
    public class DummyVehicleRepository : IVehicleRepository
    {
        private readonly ILogger<DummyVehicleRepository> _logger;

        public DummyVehicleRepository(ILogger<DummyVehicleRepository> logger)
        {
            _logger = logger;
        }

        public Task<Vehicle> Create(Vehicle vehicle)
        {
            _logger.LogInformation("Saving Vehicle in Dummy Vehicle Repository");
            vehicle.Id = 1;
            return Task.FromResult(vehicle);
        }
    }
}
