using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;


namespace FurnitureFactory.Models
{
    public class Furniture : Product
    {

        [StringLength(255, ErrorMessage = "Description can be no more than 255 characters long")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please add the product's weight.")]
        [Range(typeof(decimal), "0.00", "99999999.99", ErrorMessage = "Bad input, try again.")]
        [Min(0)]
        public decimal Weight { get; set; }

        [Required (ErrorMessage = "Please add the product's BarDocde")]
        [RegularExpression(@"[0-9A-Za-z]{4,30}")]
        [Display(Name="Bar Code")]
        public string BarCode { get; set; }

    }
}