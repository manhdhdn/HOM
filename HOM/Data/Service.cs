using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class Service
    {
        [Key]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        [ForeignKey("Hostels")]
        public string HostelId { get; set; } = null!;

        //public virtual ICollection<Bill> Bills { get; set; }
    }
}
