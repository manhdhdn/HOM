using System.ComponentModel.DataAnnotations;

namespace HOM.Data
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
