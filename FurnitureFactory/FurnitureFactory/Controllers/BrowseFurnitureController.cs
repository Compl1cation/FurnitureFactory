using System.Net;
using System.Web.Mvc;
using FurnitureFactory.Models;


namespace FurnitureFactory.Controllers
{
    //Implementing Base Controller and using it's Index ActionResults
    public class BrowseFurnitureController : BaseController
    {
        protected override string _partView { get; set; }
        public BrowseFurnitureController()
        {
            _partView = "_Products";
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
