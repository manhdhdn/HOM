using System.ComponentModel.DataAnnotations;

namespace HOM.Data
{
    public partial class Role
    {
        [Required, Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        //public virtual ICollection<Account> Accounts { get; set; }
    }
}
