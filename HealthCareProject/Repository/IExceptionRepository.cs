namespace HealthCareProject.Repository
{
    public interface IExceptionRepository
    {
        public Task<IEnumerable<Exception>> GetAllExceptions();
        public Task<Exception> GetExceptionById(int id);
        public Task AddException(Exception exception);

    }
}
