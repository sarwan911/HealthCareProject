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
    public class DocAvailabilitiesController : ControllerBase
    {
        private readonly Context _context;

        public DocAvailabilitiesController(Context context)
        {
            _context = context;
        }

        // GET: api/DocAvailabilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocAvailability>>> GetDocAvailabilities()
        {
            return await _context.DocAvailabilities.ToListAsync();
        }

        // GET: api/DocAvailabilities/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DocAvailability>> GetDocAvailability(int id)
        {
            var docAvailability = await _context.DocAvailabilities.FindAsync(id);

            if (docAvailability == null)
            {
                return NotFound();
            }

            return docAvailability;
        }

        // PUT: api/DocAvailabilities/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocAvailability(int id, DocAvailability docAvailability)
        {
            if (id != docAvailability.SessionId)
            {
                return BadRequest();
            }

            _context.Entry(docAvailability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocAvailabilityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DocAvailabilities
        [HttpPost]
        public async Task<ActionResult<DocAvailability>> PostDocAvailability(DocAvailability docAvailability)
        {
            _context.DocAvailabilities.Add(docAvailability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocAvailability", new { id = docAvailability.SessionId }, docAvailability);
        }

        // DELETE: api/DocAvailabilities/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocAvailability(int id)
        {
            var docAvailability = await _context.DocAvailabilities.FindAsync(id);
            if (docAvailability == null)
            {
                return NotFound();
            }

            _context.DocAvailabilities.Remove(docAvailability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocAvailabilityExists(int id)
        {
            return _context.DocAvailabilities.Any(e => e.SessionId == id);
        }
    }
}