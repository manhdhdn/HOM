using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class RoomModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string RoomType { get; set; }
        [Required]
        public double Acreage { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
