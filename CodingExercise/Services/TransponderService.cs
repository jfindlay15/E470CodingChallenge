using CodingExercise.Events;
using CodingExercise.Interfaces;
using CodingExercise.Models;
using Newtonsoft.Json;

namespace CodingExercise.Services
{
    public class TransponderService : ITransponderService
    {
        private readonly ILogger<TransponderService> _logger;
        private readonly ITransponderRepositoryFactory _transponderRepositoryFactory;

        public TransponderService(ILogger<TransponderService> logger, ITransponderRepositoryFactory transponderRepositoryFactory)
        {
            _logger = logger;
            _transponderRepositoryFactory = transponderRepositoryFactory;
        }

        public Transponder Create(Vehicle vehicle)
        {
            Transponder transponder = null;
            if (vehicle != null)
            {
                var year = int.Parse(vehicle?.Year);
                var transponderRepository = _transponderRepositoryFactory.GetTransponderRepository(year);
                transponder = transponderRepository.Create(vehicle);
                _logger.LogInformation("Transponder created {transponder}", JsonConvert.SerializeObject(transponder));
            }

            return transponder;
        }

        public void OnVehicleCreated(VehicleEventArgs vehicleEventArgs)
        {
            if (vehicleEventArgs.Vehicle != null)
            {
                Create(vehicleEventArgs.Vehicle);
            }
        }
    }
}
