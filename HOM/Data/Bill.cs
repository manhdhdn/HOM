using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class Bill
    {
        [Key]
        public string Id { get; set; } = null!;
        [ForeignKey("Services")]
        public string ServiceId { get; set; } = null!;
        public DateTime Date { get; set; }
        [ForeignKey("Rooms")]
        public string RoomId { get; set; } = null!;
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public bool Status { get; set; }
    }
}
