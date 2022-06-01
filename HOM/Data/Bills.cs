using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public class Bills
    {
        [Key]
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Rooms")]
        public int RoomId { get; set; }
        [ForeignKey("Accounts")]
        public int AccountId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public bool Status { get; set; }
    }
}
