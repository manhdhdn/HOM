using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public class Accounts
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public string Gender { get; set; }
        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public string? Avatar { get; set; }
        public bool Status { get; set; }
    }
}
