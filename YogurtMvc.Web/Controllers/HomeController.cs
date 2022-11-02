using Microsoft.AspNetCore.Mvc;

namespace YogurtMvc.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
