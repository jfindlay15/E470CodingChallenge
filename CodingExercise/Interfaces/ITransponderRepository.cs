using CodingExercise.Models;

namespace CodingExercise.Interfaces
{
    public interface ITransponderRepository
    {
        Transponder Create(Vehicle vehicle);
    }
}
