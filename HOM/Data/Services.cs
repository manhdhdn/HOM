using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public class Services
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        [ForeignKey("Hostels")]
        public int HostelId { get; set; }
    }
}
