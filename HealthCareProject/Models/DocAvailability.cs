using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthCareProject.Models
{
    public class DocAvailability
    {
        [Key]
        public int SessionId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateOnly AvailableDate { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
        [Required]
        public string Location { get; set; }
    } 
}