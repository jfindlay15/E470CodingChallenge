using CodingExercise.Events;
using CodingExercise.Interfaces;
using CodingExercise.Models;

namespace CodingExercise.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ILogger<VehicleService> _logger;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITransponderService _transponderService;
        public event EventHandler<VehicleEventArgs> VehicleCreated;

        public VehicleService(ILogger<VehicleService> logger, IVehicleRepository vehicleRepository, ITransponderService transponderService)
        {
            _logger = logger;
            _vehicleRepository = vehicleRepository;
            _transponderService = transponderService;

            VehicleCreated += VehicleService_VehicleCreated;
        }

        private void VehicleService_VehicleCreated(object sender, VehicleEventArgs e)
        {
            _transponderService.OnVehicleCreated(e);
        }

        public async Task<Vehicle> Create(Vehicle vehicle)
        {

            Vehicle newVehicle = null;

            if (vehicle != null)
            {
                newVehicle = await _vehicleRepository.Create(vehicle);

                var newVehicleArgs = new VehicleEventArgs { Vehicle = newVehicle };
                OnVehicleCreated(newVehicleArgs);
            }

            return newVehicle;
        }

        protected virtual void OnVehicleCreated(VehicleEventArgs e)
        {
            VehicleCreated?.Invoke(this, e);
        }
    }
}
