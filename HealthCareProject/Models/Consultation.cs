using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthCareProject.Models
{
    public class Consultation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConsultationId { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public string Notes { get; set; }
        public string Prescription { get; set; }
        public DateOnly ConsultationDate { get; set; }
    }
}