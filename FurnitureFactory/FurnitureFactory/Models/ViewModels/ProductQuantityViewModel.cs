using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace FurnitureFactory.Models.ViewModels
{
    public class ProductQuantityViewModel
    {
        [Required]
        [XmlAttribute(attributeName: "FurnitureId")]
        public int FurnitureId { get; set; }
        [XmlAttribute(attributeName: "Quantity")]
        [Required]
        [Range(typeof(int), "1", "300", ErrorMessage = "Quantity can be between 1 and 300")]
        public int Quantity { get; set; }

    }
}