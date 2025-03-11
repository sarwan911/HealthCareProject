using Microsoft.EntityFrameworkCore;
using HealthCareProject.Repository;
using HealthCareProject;
using HealthCareProject.Models;
using HealthCareProject.Data;
using HealthCareProject.Authentication;

namespace HealthCareProject.Repository
{
    public class DocAvailabilityRepository : IDocAvailabilityRepository
    {
        private readonly Context _context;

        public DocAvailabilityRepository(Context context)
        {
            _context = context;
        }

        // 📌 1️⃣ Add Doctor Availability
        public async Task<DocAvailability> AddAvailabilityAsync(DateOnly availableDate, TimeOnly startTime, TimeOnly endTime, string location)
        {
            return await _context.DocAvailabilities
                .FromSqlRaw("EXEC sp_AddDoctorAvailability @p0, @p1, @p2, @p3",
                    availableDate, startTime, endTime, location)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        // 📌 2️⃣ Get All Available Sessions
        public async Task<List<DocAvailability>> GetAvailableSessionsAsync()
        {
            return await _context.DocAvailabilities
                .FromSqlRaw("EXEC sp_GetAvailableSessions")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<DocAvailability> GetByIdAsync(int id)
        {
            return await _context.DocAvailabilities.FindAsync(id);
        }

        public async Task<List<DocAvailability>> GetAllAsync()
        {
            return await _context.DocAvailabilities.ToListAsync();
        }

        public async Task AddAsync(DocAvailability availability)
        {
            await _context.DocAvailabilities.AddAsync(availability);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocAvailability availability)
        {
            _context.DocAvailabilities.Update(availability);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var availability = await _context.DocAvailabilities.FindAsync(id);
            if (availability != null)
            {
                _context.DocAvailabilities.Remove(availability);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<DocAvailability>> GetAvailabilityForDoctor(int doctorId)
        {
            return await _context.DocAvailabilities.Where(d => d.DoctorId == doctorId).ToListAsync();
        }
    }
}