using DataAnnotationsExtensions;
using FurnitureFactory.Areas.HelpPage.ModelDescriptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureFactory.Models
{
    [ModelName("Furniture")]
    public abstract class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the product's name")]
        [StringLength(255, ErrorMessage = "Name cannot be longer than 255 characters.")]
        public string Name { get; set; }


        [Range(typeof(decimal), "0.00", "99999999.99", ErrorMessage = "Bad input, try again.")]
        [Min(0)]
        [Display(Name="Price")]
        public decimal PricePerUnit { get; set; }

        public virtual ICollection<FurnitureOrder> FurnitureOrders { get; set; }
    }
}