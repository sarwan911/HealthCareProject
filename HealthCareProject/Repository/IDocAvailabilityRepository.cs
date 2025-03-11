using HealthCareProject.Models;
using HealthCareProject.Repository;

namespace HealthCareProject.Repository
{
    public interface IDocAvailabilityRepository
    {
        public interface IDocAvailabilityRepository
        {
            Task<DocAvailability> AddAvailabilityAsync(DateOnly availableDate, TimeOnly startTime, TimeOnly endTime, string location);
            Task<List<DocAvailability>> GetAvailableSessionsAsync();
            Task<DocAvailability> GetByIdAsync(int id);
            Task<List<DocAvailability>> GetAllAsync();
            Task AddAsync(DocAvailability availability);
            Task UpdateAsync(DocAvailability availability);
            Task DeleteAsync(int id);
            Task<List<DocAvailability>> GetAvailabilityForDoctor(int doctorId);
        }
    }
}
