using CodingExercise.Events;
using CodingExercise.Models;

namespace CodingExercise.Interfaces
{
    public interface ITransponderService
    {
        Transponder Create(Vehicle vehicle);
        void OnVehicleCreated(VehicleEventArgs vehicleEventArgs);
    }
}
