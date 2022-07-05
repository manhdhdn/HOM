using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class Booking
    {
        [Required, Key]
        public string Id { get; set; } = null!;
        [Required, ForeignKey("Accounts")]
        public string AccountId { get; set; } = null!;
        [Required, ForeignKey("Rooms")]
        public string RoomId { get; set; } = null!;
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public DateTime MeetingDate { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
