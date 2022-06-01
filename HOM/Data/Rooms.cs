using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOM.Data
{
    public class Rooms
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Images")]
        public int ImageId { get; set; }
        [ForeignKey("RoomType")]
        public int TypeId { get; set; }
        public double Acreage { get; set; }
        public double Price { get; set; }
        [ForeignKey("Accounts")]
        public int? AccountId { get; set; }
        [ForeignKey("Hostels")]
        public int HostelId { get; set; }
        public bool Status { get; set; }
    }

    public class RoomType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
