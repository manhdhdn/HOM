using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class Image
    {
        [Required, Key]
        public string Id { get; set; } = null!;
        [Required]
        public string Url { get; set; } = null!;
        [Required, ForeignKey("Hostels")]
        public string HostelId { get; set; } = null!;
        [ForeignKey("Rooms")]
        public string? RoomId { get; set; }
    }
}
