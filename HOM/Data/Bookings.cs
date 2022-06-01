using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public class Bookings
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Accounts")]
        public int AccountId { get; set; }
        [ForeignKey("Rooms")]
        public int RoomId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime MeetingDate { get; set; }
        public bool Status { get; set; }
    }
}
