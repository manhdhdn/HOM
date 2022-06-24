using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
