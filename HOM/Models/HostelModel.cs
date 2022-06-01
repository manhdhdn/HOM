using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class HostelModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string LandlordsName { get; set; }
    }
}
