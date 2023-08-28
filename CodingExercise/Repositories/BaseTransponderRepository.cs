using CodingExercise.Models;

namespace CodingExercise.Repositories
{
    public abstract class BaseTransponderRepository
    {
        public Transponder Create(Vehicle vehicle)
        {
            var transponder = new Transponder
            {
                Id = 1,
                VehicleId = vehicle.Id,
            };

            return transponder;
        }
    }
}
