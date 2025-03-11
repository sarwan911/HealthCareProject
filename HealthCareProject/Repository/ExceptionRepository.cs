
using HealthCareProject.Authentication;

namespace HealthCareProject.Repository
{
    public class ExceptionRepository : IExceptionRepository
    {
        public Task AddException(Exception exception)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Exception>> GetAllExceptions()
        {
            throw new NotImplementedException();
        }

        public Task<Exception> GetExceptionById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
