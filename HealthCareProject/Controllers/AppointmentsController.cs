using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthCareProject.Data;
using HealthCareProject.Models;
using HealthCareProject.Repository;

namespace HealthCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly Context _context;

        public AppointmentsController(Context context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        // POST: api/Appointments/Book
        [HttpPost("Book_An_Appointment")]
        public async Task<IActionResult> BookAppointment(int sessionId, int patientId)
        {
            var session = await _context.DocAvailabilities.FindAsync(sessionId);
            if (session == null)
            {
                return BadRequest("Invalid session ID.");
            }

            var appointment = new Appointment
            {
                SessionId = sessionId,
                PatientId = patientId,
                Status = "Booked"
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            var notification = new Notification
            {
                UserId = patientId,
                Type = NotificationType.AppointmentBooked,
                Message = $"Your appointment is booked with Dr. {session.DoctorId} at {session.Location} on {session.AvailableDate}.",
                Status = "Booked"
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok("Appointment booked successfully.");
        }

        // GET: api/Appointments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // PUT: api/Appointments/{appointmentId}/reschedule
        [HttpPut("{appointmentId}/reschedule")]
        public async Task<IActionResult> RescheduleAppointment(int appointmentId, int newSessionId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
            {
                return NotFound("Appointment not found.");
            }

            var newSession = await _context.DocAvailabilities.FindAsync(newSessionId);
            if (newSession == null)
            {
                return BadRequest("Invalid session ID.");
            }

            appointment.SessionId = newSessionId;
            appointment.Status = "Rescheduled";
            await _context.SaveChangesAsync();

            var notification = new Notification
            {
                UserId = appointment.PatientId,
                Type = NotificationType.AppointmentRescheduled,
                Message = $"Your appointment has been rescheduled to {newSession.AvailableDate} at {newSession.Location}.",
                Status = "Rescheduled"
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok("Appointment rescheduled successfully.");
        }

        // POST: api/Appointments
        //[HttpPost]
        //public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        //{
        //    _context.Appointments.Add(appointment);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAppointment", new { id = appointment.AppointmentId }, appointment);
        //}

        // DELETE: api/Appointments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}