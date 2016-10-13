using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FurnitureFactory.Models;
using FurnitureFactory.Models.ViewModels;

namespace FurnitureFactory.Controllers
{
    [Authorize(Roles = "ContentAdmin, MasterAdmin")]
    public class UserManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserManager
        public virtual ActionResult Autocomplete(string term)
        {
            var model = db.Users.Where(f => f.UserName.StartsWith(term))
                .Take(10)
                .Select(f => new { label = f.UserName });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Page, Next, Previous, Header sorting
        public ActionResult Index(SortingInfo sortInfo, string sortField = "UserName", int pageIndex = 1, string searchTerm = null)
        {

            SortingInfo.SetProperties(sortInfo, pageIndex, searchTerm, sortField);

            ViewBag.SortingInfo = sortInfo;
            ViewBag.Direction = sortInfo.SortDirection;

            IQueryable<ApplicationUser> users = sortInfo.SortQuery(db.Users
                        .Where(p => sortInfo.SearchTerm == null || p.UserName.StartsWith(sortInfo.SearchTerm)))
                        .Skip((sortInfo.CurrentPageIndex - 1) * sortInfo.PageSize)
                        .Take(sortInfo.PageSize);

            var model = users.ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ManageUsers", model);
            }
            return View(model);
        }

        // POST: Search
        [HttpPost]
        public ActionResult Index(SortingInfo sortInfo, string searchTerm, string sortField = "UserName")
        {
            sortInfo.SortField = sortField;
            sortInfo.SearchTerm = searchTerm;
            sortInfo.CurrentPageIndex = 1;

            ViewBag.SortingInfo = sortInfo;

            IQueryable<ApplicationUser> furniture = sortInfo.SortQuery(db.Users
                .Where(p => sortInfo.SearchTerm == null || p.UserName.StartsWith(sortInfo.SearchTerm)))
                .Take(sortInfo.PageSize);
            var model = furniture.ToList();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ManageUsers", model);
            }

            return HttpNotFound();
        }
    // GET: UserManager/Details/5
    public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: UserManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Adress,Bulstat,DDSReg,MOL,OrderId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: UserManager/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: UserManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FullName,Adress,Bulstat,DDSReg,MOL,Email,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: UserManager/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: UserManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
