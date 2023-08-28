using CodingExercise.Models;

namespace CodingExercise.Events
{
    public class VehicleEventArgs : EventArgs
    {
        public Vehicle Vehicle { get; set; }
    }
}
