using CodingExercise.Interfaces;

namespace CodingExercise.Repositories
{
    public class TransponderRepositoryFactory : ITransponderRepositoryFactory
    {
        private readonly ILogger<TransponderRepositoryFactory> _logger;

        public TransponderRepositoryFactory(ILogger<TransponderRepositoryFactory> logger)
        {
            _logger = logger;
        }

        public ITransponderRepository GetTransponderRepository(int year)
        {
            ITransponderRepository transponderRepository;

            if (year <= DateTime.Now.Year - 25)
            {
                transponderRepository = new ClassicTransponderRepository();
                //_serviceProvider.GetServices<ITransponderRepository>().First(a => a.GetType() == typeof(ClassicTransponderRepository));
                _logger.LogInformation($"Getting the ClassicTransponderRepository from the {typeof(TransponderRepositoryFactory)} class.");
            }
            else
            {
                transponderRepository = new ModernTransponderRepository();
                //_serviceProvider.GetServices<ITransponderRepository>().First(a => a.GetType() == typeof(ModernTransponderRepository));
                _logger.LogInformation($"Getting the ModernTransponderRepository from the {typeof(TransponderRepositoryFactory)} class.");
            }

            return transponderRepository;
        }
    }
}
