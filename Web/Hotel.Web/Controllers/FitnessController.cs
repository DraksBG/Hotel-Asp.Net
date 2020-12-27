namespace Hotel.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class FitnessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
