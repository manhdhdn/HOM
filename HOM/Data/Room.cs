using HOM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class Room
    {
        public Room()
        {
        }
        public Room(RoomModel room, bool method)
        {
            Id = method ? Guid.NewGuid().ToString() : room.Id;
            Name = room.Name;
            RoomTypeId = room.RoomTypeId;
            HostelId = room.HostelId;
            Status = room.Status;
        }

        [Required, Key]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required, ForeignKey("RoomTypes")]
        public string RoomTypeId { get; set; } = null!;
        [Required, ForeignKey("Hostels")]
        public string HostelId { get; set; } = null!;
        [Required]
        public bool Status { get; set; }

        //public virtual ICollection<Bill> Bills { get; set; }
        //public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        //public virtual ICollection<RoomsMembership> RoomsMemberships { get; set; }
    }
}
