using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace FurnitureFactory.Models.ViewModels
{
    public class ProductQuantityViewModel
    {
  
        [XmlAttribute(attributeName: "FurnitureId")]
        public int FurnitureId { get; set; }
        [XmlAttribute(attributeName: "Quantity")]
        public int Quantity { get; set; }

    }
}