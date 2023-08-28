using CodingExercise.Events;
using CodingExercise.Models;

namespace CodingExercise.Interfaces
{
    public interface IVehicleService
    {
        event EventHandler<VehicleEventArgs> VehicleCreated;
        Task<Vehicle> Create(Vehicle vehicle);
    }
}
