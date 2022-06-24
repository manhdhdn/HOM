using System.ComponentModel.DataAnnotations;

namespace HOM.Data
{
    public partial class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        //public virtual ICollection<Account> Accounts { get; set; }
    }
}
