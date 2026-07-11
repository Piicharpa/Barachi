using System;

namespace Barachi.Models
{
    // Maps to the "RetapingRecords" table — logs that the user acknowledged
    // the retaping instructions for a given RBID
    public class RetapingRecord
    {
        public int Id { get; set; }
        public string RBID { get; set; }
        public string LotNumber { get; set; }
        public string Instructions { get; set; }
        public DateTime AcknowledgedDate { get; set; }
        public string Status { get; set; }
    }
}