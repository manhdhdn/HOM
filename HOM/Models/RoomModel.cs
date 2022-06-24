namespace HOM.Models
{
    public class RoomModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string RoomTypeId { get; set; } = null!;
        public double Acreage { get; set; }
        public double Price { get; set; }
        public string HostelId { get; set; } = null!;
        public bool Status { get; set; }
    }
}
