using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthCareProject.Models
{
    public enum NotificationType
    {
        AppointmentBooked,
        AppointmentRescheduled,
        AppointmentCanceled,
        DoctorNotAvailable,
        PrescriptionSent
    }
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public NotificationType Type { get; set; }
        [Required]
        public string Message { get; set; }
        public DateOnly CreatedAt { get; set; }
        [Required]
        public string Status { get; set; } // Booked, Rescheduled, Canceled, etc.
    }
}