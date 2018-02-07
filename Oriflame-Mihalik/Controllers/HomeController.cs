using System.Web.Mvc;

namespace Oriflame_Mihalik.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Oriflame-Mihalik";

            return View();
        }
    }
}
