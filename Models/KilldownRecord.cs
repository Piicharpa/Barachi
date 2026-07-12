using System;

namespace Barachi.Models
{
    // Maps to the "KilldownRecords" table — logs that the user acknowledged
    // the killdown instructions for a given RBID
    public class KilldownRecord
    {
        public long Id { get; set; }
        public string RBID { get; set; }
        public string LotNumber { get; set; }
        public string Instructions { get; set; }
        public DateTime AcknowledgedDate { get; set; }
        public string Status { get; set; }
    }
}