using DapperWithSQL.IRepository;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace DapperWithSQL.Controllers
{
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IImageRepository _imageRepository;

        public ImageController(IWebHostEnvironment environment, IImageRepository imageRepository)
        {
            _environment = environment;
            _imageRepository = imageRepository;
        }

        private ImageVM GetDropdown()
        {
            IEnumerable<StaticValue> staticValues = new List<StaticValue>();
            staticValues = _imageRepository.GetStaticValue();
            var model = new ImageUploadAndListVM();
            model.imageVM.SelectStaticValueType = new List<SelectListItem>();
            foreach (StaticValue staticValue in staticValues)
            {
                model.imageVM.SelectStaticValueType.Add(new SelectListItem { Text = staticValue.static_data, Value = staticValue.sno.ToString() });
            }
            return model.imageVM;
        }

        public IActionResult Index()
        {
            //IEnumerable<StaticValue> staticValues = new List<StaticValue>();
            //staticValues = _imageRepository.GetStaticValue();
            var model = new ImageUploadAndListVM();
            model.imageVM.SelectStaticValueType = new List<SelectListItem>();
            //foreach (StaticValue staticValue in staticValues)
            //{
            //    model.imageVM.SelectStaticValueType.Add(new SelectListItem { Text = staticValue.static_data, Value = staticValue.sno.ToString() });
            //}
            model.Images = _imageRepository.GetImage();
            model.imageVM = GetDropdown();
            return View(model);
        }
        [HttpGet]
        public JsonResult AjaxMethod(string id,string value) //string type,
        {
            if (string.IsNullOrEmpty(id))
            {
                // Handle the case when id is null or empty
                return Json(new List<SelectListItem>());  // Return empty list 
            }
           
            JsonResult jsonData = _imageRepository.AjaxMethod(id, value);
            return jsonData;
           
        }

        [HttpPost]
        public async Task<IActionResult> Index(ImageUploadAndListVM image)
        {
            if (image.imageVM?.ImageData == null)
            {
                ViewBag.Message = "No file uploaded.";
                return View();
            }
            try
            {
                Api_Response response = await _imageRepository.AddImage(image.imageVM);
                ViewBag.Message = $"{response.Msg} uploaded successfully.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error uploading file: {ex.Message}";
            }
            var model = new ImageUploadAndListVM();
            model.imageVM.SelectStaticValueType = new List<SelectListItem>();
            model.Images = _imageRepository.GetImage();
            model.imageVM = GetDropdown();
            return View(new ImageUploadAndListVM());
        }
        [HttpGet]
        public IActionResult GetImages()
        {
            return View(); 
        }
    }
}
