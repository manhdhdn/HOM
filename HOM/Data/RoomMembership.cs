using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class RoomMembership
    {
        [Key]
        public string Id { get; set; } = null!;
        [ForeignKey("Accounts")]
        public string AccountId { get; set; } = null!;
        [ForeignKey("Rooms")]
        public string RoomId { get; set; } = null!;
    }
}
