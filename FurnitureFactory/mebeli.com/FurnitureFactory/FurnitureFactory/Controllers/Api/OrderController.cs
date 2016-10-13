using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using FurnitureFactory.Models;
using FurnitureFactory.Models.ViewModels;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Microsoft.AspNet.Identity;

namespace FurnitureFactory.Controllers.Api
{
    public class OrderController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET api/<controller>
        public IEnumerable<OrderDTO> GetOrders()
        {
            var orders = from o in db.Order
                         select new OrderDTO()
                         {
                             ClientId = o.ClientId,
                             Date = o.Date,
                             TotalPrice = o.TotalPrice,
                             RecieptNumber = o.RecieptNumber
                         };
            return orders;
        }

        // GET api/<controller>/5
        public OrderDTO GetOrder(int id)
        {
            var order = (from o in db.Order
                         where o.Id == id
                         select new OrderDTO()
                         {
                             ClientId = o.ClientId,
                             Date = o.Date,
                             TotalPrice = o.TotalPrice,
                             RecieptNumber = o.RecieptNumber
                         }).Single();
            return order;
        }

        // POST api/<controller>
        // <summary>
        //jhkjhkjhkjhkh
        //</summary>
        // <param name = "val" > Values </ param >
        [HttpPost]
        public bool PostOrder([FromBody] ProductQuantityViewModel[] prods)
        {

            try
            {
                Order order = new Order();
                order.ClientId = User.Identity.GetUserId();
                order.Date = DateTime.Now;
                order.TotalPrice = 0;
                order.RecieptNumber = RandomString(9);

                order.FurnitureOrders = new List<FurnitureOrder>();
                foreach (var item in prods)
                {
                    order.FurnitureOrders.Add(new FurnitureOrder(){
                        FurnitureId = item.FurnitureId,
                        Quantity = item.Quantity
                     });
                    order.TotalPrice += (db.Furniture.Where(f => f.Id == item.FurnitureId).Select(f => f.PricePerUnit).SingleOrDefault())*(item.Quantity);
                }

                db.Order.Add(order);
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return false;
        }
        // PUT api/<controller>/5
        /// <summary>
        /// jhgjhgjhgj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}