using Microsoft.AspNetCore.Mvc;

namespace TourismMVC.Controllers
{
    public class Place_PhotosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
