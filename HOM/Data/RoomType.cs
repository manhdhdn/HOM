using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class RoomType
    {
        [Required, Key]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public double Acreage { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool Furniture { get; set; }
        public string? Description { get; set; }
        [Required, ForeignKey("Hostels")]
        public string HostelId { get; set; } = null!;

        //public virtual ICollection<Room> Rooms { get; set; }
    }
}
