namespace HOM.Models
{
    public class HostelModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        public bool Status { get; set; }
    }
}
