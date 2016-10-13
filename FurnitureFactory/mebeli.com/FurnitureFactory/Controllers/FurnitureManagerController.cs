using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FurnitureFactory.Models;

namespace FurnitureFactory.Controllers
{
    [Authorize]
    public class FurnitureManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Furniture
        public ActionResult Index()
        {
            return View(db.Furniture.Take(50).ToList());
        }

        // GET: Furniture/Details/5

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

        // GET: Furniture/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Furniture/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PricePerUnit,Description,Weight,BarCode")] Furniture furniture)
        {
            if (ModelState.IsValid)
            {
                db.Furniture.Add(furniture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(furniture);
        }

        // GET: Furniture/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Furniture/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PricePerUnit,Description,Weight,BarCode")] Furniture furniture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(furniture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(furniture);
        }

        // GET: Furniture/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Furniture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Furniture furniture = db.Furniture.Find(id);
            db.Furniture.Remove(furniture);
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
