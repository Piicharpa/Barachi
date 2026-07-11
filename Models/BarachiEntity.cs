namespace Barachi.Models
{
    public class BarachiEntity
    {
        public int Id { get; set; }
        public string RBID { get; set; }
        public string LotNumber { get; set; }
        public string Type { get; set; }        // e.g. "15mg"
        public int Quantity { get; set; }
        public string Status { get; set; }       // Active, Deleted, KilldownPending, Retaping
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}