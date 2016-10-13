using FurnitureFactory.Models;
using System.Web.Mvc;


namespace FurnitureFactory.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart

        public ActionResult Index()
        {

            return View();
        }
    }
}