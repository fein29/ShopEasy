using Microsoft.AspNetCore.Mvc;

namespace ShopEasy.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
