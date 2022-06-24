using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class SignUpModel
    {
        public string Name { get; set; }
        [RegularExpression(@"^(Male|Female)$")]
        public string Gender { get; set; }
        [Phone]
        public string Phone { get; set; }
        public int RoleId { get; set; }
        [Url]
        public string? Avatar { get; set; }
        [RegularExpression(@"^.*(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).*$", ErrorMessage = "Password include Lowercase, Uppercase, Number and Symbol.")]
        [Compare("ComfirmPassword")]
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
    }
}
