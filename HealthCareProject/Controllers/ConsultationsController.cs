using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthCareProject.Data;
using HealthCareProject.Models;

namespace HealthCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationsController : ControllerBase
    {
        private readonly Context _context;

        public ConsultationsController(Context context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<Consultation>> PostConsultation(Consultation consultation)
        {
            _context.Consultations.Add(consultation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsultation", new { id = consultation.ConsultationId }, consultation);
        }
        // GET: api/Consultations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consultation>>> GetConsultations()
        {
            return await _context.Consultations.ToListAsync();
        }

            // GET: api/Consultations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Consultation>> GetConsultation(int id)
        {
            var consultation = await _context.Consultations.FindAsync(id);

            if (consultation == null)
            {
                return NotFound();
            }

            return consultation;
        }
        

            // DELETE: api/Consultations/{id}
            [HttpDelete("{id}")]
            public  async Task<IActionResult> DeleteConsultation(int id)
            {
                var consultation = await _context.Consultations.FindAsync(id);
                if (consultation == null)
                {
                    return NotFound();
                }

                _context.Consultations.Remove(consultation);
                await _context.SaveChangesAsync();

                return NoContent();
            }

        private bool ConsultationExists(int id)
        {
            return _context.Consultations.Any(e => e.ConsultationId == id);
        }
    }
}