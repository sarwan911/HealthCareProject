using HealthCareProject.Models;
namespace HealthCareProject.Repository
{
    public interface IConsultationRepository
    {
        Task<Consultation> GetByIdAsync(int consultationId);
        Task<List<Consultation>> GetAllAsync();
        Task AddAsync(Consultation consultation);
        Task UpdateAsync(Consultation consultation);
        Task DeleteAsync(int consultationId);
    }
}