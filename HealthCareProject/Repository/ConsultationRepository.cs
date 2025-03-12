using Microsoft.EntityFrameworkCore;
using HealthCareProject.Data;
using HealthCareProject.Repository;
using HealthCareProject.Models;

namespace HealthCareProject.Repository
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly Context _context;

        public ConsultationRepository(Context context)
        {
            _context = context;
        }

        // 📌 1️⃣ Add a Consultation Record
        public async Task<Consultation> AddConsultationAsync(int appointmentId, int doctorId, string notes, string prescription)
        {
            return await _context.Consultations
                .FromSqlRaw("EXEC sp_AddConsultation @p0, @p1, @p2, @p3",
                    appointmentId, doctorId, notes, prescription)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        // 📌 2️⃣ Get Consultation by ID
        public async Task<Consultation> GetConsultationByIdAsync(int consultationId)
        {
            return await _context.Consultations
                .FromSqlRaw("EXEC sp_GetConsultationById @p0",
                    consultationId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }


        public async Task<List<Consultation>> GetAllAsync()
        {
            return await _context.Consultations.ToListAsync();
        }
        public async Task<Consultation> GetByIdAsync(int consultationId)
        {
            return await _context.Consultations.FindAsync(consultationId);
        }
        public async Task AddAsync(Consultation consultation)
        {
            await _context.Consultations.AddAsync(consultation);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Consultation consultation)
        {
            _context.Consultations.Update(consultation);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int consultationId)
        {
            var consultation = await _context.Consultations.FindAsync(consultationId);
            if (consultation != null)
            {
                _context.Consultations.Remove(consultation);
                await _context.SaveChangesAsync();
            }
        }
    }
}