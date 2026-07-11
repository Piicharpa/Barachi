using System.ComponentModel.DataAnnotations;

namespace BarachiModel.Models
{
    public class Barachi
    {
        [Key]
        public int BarachiID { get; set; }
        public string Type { get; set; } 
        public string Lot { get; set; }
        public string Date { get; set; } = string.Empty;
        public string PIC { get; set; } = string.Empty;
        public string Reasons { get; set; } = string.Empty;
        

    }
}