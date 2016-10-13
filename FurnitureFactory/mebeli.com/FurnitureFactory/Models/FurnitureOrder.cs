
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace FurnitureFactory.Models
{
    public class FurnitureOrder
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int FurnitureId { get; set; }
        public virtual Furniture Furniture { get; set; }
        [Required(ErrorMessage ="Quantity is required")]
        [Min(0)]
        public int Quantity { get; set; }
    }
}