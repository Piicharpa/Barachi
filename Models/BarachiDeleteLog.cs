using System;

namespace Barachi.Models
{
    // Maps to the "BarachiDeleteLogs" table — audit trail of every delete confirmation
    public class BarachiDeleteLog
    {
        public int Id { get; set; }
        public string RBID { get; set; }
        public string PIC { get; set; }
        public string Reasons { get; set; }
        public DateTime DeletedDate { get; set; }
        public string NextStep { get; set; }     // "Killdown" or "Retaping"
    }
}