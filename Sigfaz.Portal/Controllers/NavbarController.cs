using System.Linq;
using System.Web.Mvc;
using Sigfaz.Portal.Domain;

namespace Sigfaz.Portal.Controllers
{
    public class NavbarController : Controller
    {
        // GET: Navbar
        public ActionResult Index()
        {
            var data = new Data();            
            return PartialView("_Navbar", data.NavbarItems().ToList());
        }
    }
}