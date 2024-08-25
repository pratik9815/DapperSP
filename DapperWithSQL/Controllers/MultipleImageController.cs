using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperWithSQL.Controllers
{
    public class MultipleImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MultipleImageModel model)
        {

            return View(model);
        }
    }
}
