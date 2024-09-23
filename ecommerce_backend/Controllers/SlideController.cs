using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    public class SlideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
