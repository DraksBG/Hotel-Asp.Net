using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class FitnessController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
