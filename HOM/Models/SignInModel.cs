using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class SignInModel
    {
        [Required, Phone]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
