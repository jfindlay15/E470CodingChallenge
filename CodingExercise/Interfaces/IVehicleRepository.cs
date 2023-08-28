using CodingExercise.Models;

namespace CodingExercise.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> Create(Vehicle vehicle);
    }
}
