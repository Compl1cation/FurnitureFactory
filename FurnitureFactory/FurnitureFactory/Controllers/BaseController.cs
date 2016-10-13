using FurnitureFactory.Models;
using FurnitureFactory.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;


namespace FurnitureFactory.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        protected abstract string _partView { get; set; }
        public virtual ActionResult Autocomplete(string term)
        {
            var model = db.Furniture.Where(f => f.Name.StartsWith(term))
                .Take(10)
                .Select(f => new { label = f.Name });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Page, Next, Previous, Header sorting
        public ActionResult Index(SortingInfo sortInfo, string sortField = "Name", int pageIndex = 1, string searchTerm = null)
        {
            SortingInfo.SetProperties(sortInfo, pageIndex, searchTerm, sortField);

            ViewBag.SortingInfo = sortInfo;
            ViewBag.Direction = sortInfo.SortDirection;

            IQueryable<Furniture> furniture = sortInfo.SortQuery(db.Furniture
                        .Where(p => sortInfo.SearchTerm == null || p.Name.StartsWith(sortInfo.SearchTerm)))
                        .Skip((sortInfo.CurrentPageIndex - 1) * sortInfo.PageSize)
                        .Take(sortInfo.PageSize);

            var model = furniture.ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView(_partView, model);
            }
            return View(model);
        }

        // POST: Search
        [HttpPost]
        public ActionResult Index(SortingInfo sortInfo, string searchTerm)
        {
            sortInfo.SearchTerm = searchTerm;
            sortInfo.CurrentPageIndex = 1;

            ViewBag.SortingInfo = sortInfo;

            IQueryable<Furniture> furniture = sortInfo.SortQuery(db.Furniture
                .Where(p => sortInfo.SearchTerm == null || p.Name.StartsWith(sortInfo.SearchTerm)))
                .Take(sortInfo.PageSize);
            var model = furniture.ToList();

            if (Request.IsAjaxRequest())
            {
                return PartialView(_partView, model);
            }

            return HttpNotFound();
        }
    }
}