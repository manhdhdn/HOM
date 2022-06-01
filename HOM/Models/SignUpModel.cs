using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class SignUpModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
        [Required]
        public int RoleId { get; set; }
        public string? Avatar { get; set; }
        [Required]
        [Compare("ComfirmPassword")]
        public string Password { get; set; }
        [Required]
        public string ComfirmPassword { get; set; }
    }
}
