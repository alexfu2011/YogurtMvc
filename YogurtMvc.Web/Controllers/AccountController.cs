using Microsoft.AspNetCore.Mvc;

namespace YogurtMvc.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
