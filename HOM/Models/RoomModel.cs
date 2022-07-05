using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class RoomModel
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string RoomTypeId { get; set; } = null!;
        [Required]
        public string HostelId { get; set; } = null!;
        [Required]
        public bool Status { get; set; }
    }
}
