using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public partial class RoomType
    {
        [Key]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double Acreage { get; set; }
        public double Price { get; set; }
        public bool Furniture { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Hostels")]
        public string HostelId { get; set; } = null!;

        //public virtual ICollection<Room> Rooms { get; set; }
    }
}
