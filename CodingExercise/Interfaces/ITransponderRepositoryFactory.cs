namespace CodingExercise.Interfaces
{
    public interface ITransponderRepositoryFactory
    {
        ITransponderRepository GetTransponderRepository(int year);
    }
}
