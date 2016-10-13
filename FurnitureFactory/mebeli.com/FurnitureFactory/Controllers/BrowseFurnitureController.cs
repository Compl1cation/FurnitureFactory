using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FurnitureFactory.Models;

namespace FurnitureFactory.Controllers
{
    public class BrowseFurnitureController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Autocomplete(string term)
        {
            var model = db.Furniture.Where(f => f.Name.StartsWith(term))
                .Take(10)
                .Select(f => new { label = f.Name });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: BrowseFurniture
        public ActionResult Index(string searchTerm = null)
        {
            var furniture = db.Furniture
                .Where(p => searchTerm == null || p.Name.StartsWith (searchTerm))
                .Take(50).ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Products", furniture);
            }
            return View(furniture);
        }

        // GET: BrowseFurniture/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Furniture furniture = db.Furniture.Find(id);
            if (furniture == null)
            {
                return HttpNotFound();
            }
            return View(furniture);
        }

    }
}
