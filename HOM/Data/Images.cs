using System.ComponentModel.DataAnnotations;

namespace HOM.Data
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
