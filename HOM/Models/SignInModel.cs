using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class SignInModel
    {
        [Phone]
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
