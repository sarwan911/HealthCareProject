using System.ComponentModel.DataAnnotations;

namespace HealthCareProject.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Role { get; set; } // Doctor or Patient
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public long Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Password { get; set; }
    }
}