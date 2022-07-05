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

        [Required, Key]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required, Phone]
        public string Phone { get; set; } = null!;
        [Required, RegularExpression(@"^(Male|Female)$")]
        public string Gender { get; set; } = null!;
        [Required, ForeignKey("Roles")]
        public int RoleId { get; set; }
        [Url]
        public string? Avatar { get; set; }
        [Required]
        public bool Status { get; set; }

        //public virtual ICollection<Booking> Bookings { get; set; }
        //public virtual ICollection<Hostel> Hostels { get; set; }
        //public virtual ICollection<RoomsMembership> RoomsMemberships { get; set; }
    }
}
