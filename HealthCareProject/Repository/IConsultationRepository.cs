using HealthCareProject.Models;
namespace HealthCareProject.Repository
{
    public interface IConsultationRepository
    {
        Task<Consultation> AddConsultationAsync(int appointmentId, int doctorId, string notes, string prescription);
        Task<Consultation> GetConsultationByIdAsync(int consultationId);
        Task<Consultation> GetByIdAsync(int consultationId);
        Task<List<Consultation>> GetAllAsync();
        Task AddAsync(Consultation consultation);
        Task UpdateAsync(Consultation consultation);
        Task DeleteAsync(int consultationId);
    }
}