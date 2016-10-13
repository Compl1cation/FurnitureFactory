using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureFactory.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public string RecieptNumber { get; set; }
        public string ClientId { get; set; }
    }
}