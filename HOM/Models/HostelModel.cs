using System.ComponentModel.DataAnnotations;

namespace HOM.Models
{
    public class HostelModel
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string AccountId { get; set; } = null!;
        [Required]
        public bool Status { get; set; }
    }
}
