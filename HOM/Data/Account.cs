using HOM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class Account
    {
        public Account()
        {
        }
        
        public Account(SignUpModel signUpModel)
        {
            Id = Guid.NewGuid().ToString();
            Name = signUpModel.Name;
            Phone = signUpModel.Phone;
            Gender = signUpModel.Gender;
            RoleId = signUpModel.RoleId;
            Avatar = signUpModel.Avatar;
            Status = true;
        }

        [Key]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        [Phone]
        public string Phone { get; set; } = null!;
        [RegularExpression(@"^(Male|Female)$")]
        public string Gender { get; set; } = null!;
        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        [Url]
        public string? Avatar { get; set; }
        public bool Status { get; set; }

        //public virtual ICollection<Booking> Bookings { get; set; }
        //public virtual ICollection<Hostel> Hostels { get; set; }
        //public virtual ICollection<RoomsMembership> RoomsMemberships { get; set; }
    }
}
