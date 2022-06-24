using HOM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class Hostel
    {
        public Hostel()
        {
        }

        public Hostel(HostelModel hostel, bool method)
        {
            Id = method ? Guid.NewGuid().ToString() : hostel.Id;
            Name = hostel.Name;
            Address = hostel.Address;
            AccountId = hostel.AccountId;
            Status = hostel.Status;
        }

        [Key]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        [ForeignKey("Accounts")]
        public string AccountId { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<RoomType> RoomTypes { get; set; }
        //public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
