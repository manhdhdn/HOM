using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class Booking
    {
        [Key]
        public string Id { get; set; } = null!;
        [ForeignKey("Accounts")]
        public string AccountId { get; set; } = null!;
        [ForeignKey("Rooms")]
        public string RoomId { get; set; } = null!;
        public DateTime BookingDate { get; set; }
        public DateTime MeetingDate { get; set; }
        public bool Status { get; set; }
    }
}
