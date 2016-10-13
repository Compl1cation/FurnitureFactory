using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FurnitureFactory.Models;
using FurnitureFactory.Models.ViewModels;

namespace FurnitureFactory.Controllers
{
    [Authorize(Roles = "ContentAdmin, MasterAdmin")]
    public class OrdersManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public virtual ActionResult Autocomplete(string term)
        {
            var model = db.Order.Where(o => o.RecieptNumber.StartsWith(term))
                .Take(10)
                .Select(o => new { label = o.RecieptNumber });

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        // GET: Page, Next, Previous, Header sorting
        public ActionResult Index(SortingInfo sortInfo, string sortField = "RecieptNumber", int pageIndex = 1, string searchTerm = null)
        {
            SortingInfo.SetProperties(sortInfo, pageIndex, searchTerm, sortField);

            ViewBag.SortingInfo = sortInfo;
            ViewBag.Direction = sortInfo.SortDirection;

            IQueryable<Order> orders = sortInfo.SortQuery(db.Order.Include(o => o.Client)
                        .Where(o => sortInfo.SearchTerm == null || o.RecieptNumber.StartsWith(sortInfo.SearchTerm)))
                        .Skip((sortInfo.CurrentPageIndex - 1) * sortInfo.PageSize)
                        .Take(sortInfo.PageSize);

            var model = orders.ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ManageOrders", model);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SortingInfo sortInfo, string searchTerm, string sortField = "RecieptNumber")
        {
            sortInfo.SortField = sortField;
            sortInfo.SearchTerm = searchTerm;
            sortInfo.CurrentPageIndex = 1;

            ViewBag.SortingInfo = sortInfo;

            IQueryable<Order> orders = sortInfo.SortQuery(db.Order.Include(o => o.Client)
                        .Where(o => sortInfo.SearchTerm == null || o.RecieptNumber.StartsWith(sortInfo.SearchTerm)))
                        .Skip((sortInfo.CurrentPageIndex - 1) * sortInfo.PageSize)
                        .Take(sortInfo.PageSize);

            var model = orders.ToList();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ManageOrders", model);
            }

            return HttpNotFound();
        }
        // GET: OrdersManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

      

        // GET: OrdersManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Users, "Id", "FullName", order.ClientId);
            return View(order);
        }

        // POST: OrdersManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,TotalPrice,RecieptNumber,ClientId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Users, "Id", "FullName", order.ClientId);
            return View(order);
        }

        // GET: OrdersManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: OrdersManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
