using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class Service
    {
        [Required, Key]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        [Required, ForeignKey("Hostels")]
        public string HostelId { get; set; } = null!;

        //public virtual ICollection<Bill> Bills { get; set; }
    }
}
