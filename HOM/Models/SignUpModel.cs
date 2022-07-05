using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class SignUpModel
    {
        [Required]
        public string Name { get; set; }
        [Required, RegularExpression(@"^(Male|Female)$")]
        public string Gender { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Url]
        public string? Avatar { get; set; }
        [Required]
        [RegularExpression(@"^.*(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).*$", ErrorMessage = "Password include Lowercase, Uppercase, Number and Symbol.")]
        [Compare("ComfirmPassword")]
        public string Password { get; set; }
        [Required]
        public string ComfirmPassword { get; set; }
    }
}
