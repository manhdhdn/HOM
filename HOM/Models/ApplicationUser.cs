using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Phone]
        public string Phone { get; set; }
    }
}
