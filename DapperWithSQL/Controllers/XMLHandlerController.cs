using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Serialization;

namespace DapperWithSQL.Controllers
{
    public class XMLHandlerController : Controller
    {
        public IActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Index(UniversityInformation info)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Message = "Please fill up all the form";
                return View(info);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(UniversityInformation));

            StringBuilder sb = new StringBuilder();
            using(StringWriter sr = new StringWriter(sb))
            {
                serializer.Serialize(sr, info);
            }
            string xmlString = sb.ToString();
            ViewBag.Message = xmlString;
            return View();
            //return RedirectToAction("Index","MultipleImage");
        }
    }
}
