using DapperWithSQL.IRepository;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperWithSQL.Controllers
{
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IImageRepository _imageRepository;

        public ImageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Image image)
        {
            if (image?.ImageData == null)
            {
                ViewBag.Message = "No file uploaded.";
                return View();
            }

            try
            {
                Api_Response response = _imageRepository.AddImage(image);
                ViewBag.Message = $"{response.Msg} uploaded successfully.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error uploading file: {ex.Message}";
            }

            return View();
        }
    }
}
