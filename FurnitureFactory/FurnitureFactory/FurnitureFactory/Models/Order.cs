using DataAnnotationsExtensions;
using FurnitureFactory.Areas.HelpPage.ModelDescriptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FurnitureFactory.Models
{
    [ModelName("Order")]
    public class Order
    {
        public Order()
        {
            FurnitureOrders = new List<FurnitureOrder>();
        }
        [Key]
        public virtual int Id { get; set; }
        public DateTime Date { get; set; }

        [RegularExpression(@"^[0-9]{1,10}\.[0-9]{2}")]
        [Min(0.00)]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Please add the product's BarDocde")]
        [RegularExpression(@"[0-9A-Za-z]{1,50}")]
        public string RecieptNumber { get; set; }
        public virtual string ClientId { get; set; }
        public virtual ApplicationUser Client { get; set; }

        public virtual ICollection<FurnitureOrder> FurnitureOrders { get; set; }
        
    }
}