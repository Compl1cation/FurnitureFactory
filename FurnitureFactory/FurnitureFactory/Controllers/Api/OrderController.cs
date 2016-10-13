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
using System.Web.Http.Description;

namespace FurnitureFactory.Controllers.Api
{
    public class OrderController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // <summary>
        //Gets a list of all the orders and assigns them to an OrderDTO for viewing.
        //</summary>
        // GET api/Order
        public IHttpActionResult GetOrders()
        {
            var orders = from o in db.Order
                         select new OrderDTO()
                         {
                             Id = o.Id,
                             ClientId = o.ClientId,
                             Date = o.Date,
                             TotalPrice = o.TotalPrice,
                             RecieptNumber = o.RecieptNumber
                         };
            return Ok(orders);
        }

        // GET api/Order/5
        // <summary>
        //Gets a a single Order by it's Id
        //</summary>
        public IHttpActionResult GetOrder(int id)
        {
           var order = (from o in db.Order
                        where o.Id == id
                        select new OrderDTO()
                        {
                           Id = o.Id,
                           ClientId = o.ClientId,
                           Date = o.Date,
                           TotalPrice = o.TotalPrice,
                           RecieptNumber = o.RecieptNumber
                        }).SingleOrDefault();
                if (order == null)
                {
                    return NotFound();
                }
            return Ok(order);
        }

        // POST api/Order
        // <summary>
        //Posts an array of products with their quantities and then creates an Order
        //for the current logged in user.
        //</summary>
        // <param name = "val" > Values </ param >
        [HttpPost]
        [ResponseType(typeof(ProductQuantityViewModel))]
        public IHttpActionResult PostOrder([FromBody] ProductQuantityViewModel[] prods)
        {
            if (prods == null)
            {
                return BadRequest("Products list can't be empty");
            }
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
                return Ok("Posted sucessfully!");
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
                return InternalServerError(dbEx);
            }
        }
        // PUT api/<controller>/5
        /// <summary>
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
        
        //Generates random Reciept Number
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}