using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class SpaController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
