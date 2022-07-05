using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class RoomMembership
    {
        [Required, Key]
        public string Id { get; set; } = null!;
        [Required, ForeignKey("Accounts")]
        public string AccountId { get; set; } = null!;
        [Required, ForeignKey("Rooms")]
        public string RoomId { get; set; } = null!;
    }
}
