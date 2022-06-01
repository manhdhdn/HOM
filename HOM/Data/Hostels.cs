using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public class Hostels
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Images")]
        public int ImageId { get; set; }
        public string Address { get; set; }
        [ForeignKey("Accounts")]
        public int AccountId { get; set; }
        public bool Status { get; set; }
    }
}
