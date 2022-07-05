using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class Bill
    {
        [Required, Key]
        public string Id { get; set; } = null!;
        [Required, ForeignKey("Services")]
        public string ServiceId { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        [Required, ForeignKey("Rooms")]
        public string RoomId { get; set; } = null!;
        [Required]
        public double Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
