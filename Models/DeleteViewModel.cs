using System.ComponentModel.DataAnnotations;

namespace Barachi.Models
{
    // Bound to the Delete confirmation page
    public class DeleteViewModel
    {
        [Required(ErrorMessage = "RBID is required.")]
        [Display(Name = "RBID")]
        public string RBID { get; set; }

        [Required(ErrorMessage = "Lot number is required.")]
        [Display(Name = "Lot Number")]
        public string LotNumber { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [Display(Name = "Type")]
        public string Type { get; set; }          // e.g. "15mg" — drives branching logic

        [Required(ErrorMessage = "PIC is required.")]
        [Display(Name = "PIC")]
        public string PIC { get; set; }

        [Required(ErrorMessage = "Reasons are required.")]
        [Display(Name = "Reasons")]
        public string Reasons { get; set; }
    }
}