using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace DapperWithSQL.Controllers
{
    public class MultipleImageController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;

        public MultipleImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MultipleImageModel model)
        {
            
            return View(model);
        }

        
        public MultipartFormDataContent MultipleImagePost()
        {

            string[] filePath = { "DOC_Uploads_customerSNO_274385095233.jpg", "DOC_Uploads_customerSNO_523157478030.jpg" };
                                 // "DOC_Uploads_customerSNO_523157478030.jpg",
                                  //"DOC_Uploads_customerSNO_583551954187.jpg" };
            MultipartFormDataContent content = new MultipartFormDataContent();
            //content.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            //content.Add(new StringContent("Chris"), "Name");
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath,"Uploads");
            try
            {
                foreach (string file in filePath)
                {
                    string imagePath = Path.Combine(folderPath, file);
                    FileStream filestream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                    StreamContent streamContent = new StreamContent(filestream);
                    streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                    content.Add(streamContent, "file", Path.GetFileName(file));
                }
                var stringContent = content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           



            return content;
        }
        [HttpPost]
        public IActionResult ProcessMultipleFiles()
        {
            MultipartFormDataContent content = MultipleImagePost();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // https://api.escuelajs.co/api/v1/files/upload
                    HttpResponseMessage msg = httpClient.PostAsync("https://localhost:44348/api/ImageUpload/upload", content).Result;
                    string responseStr = msg.Content.ReadAsStringAsync().Result;
                }
                var stringContent = content.ReadAsStringAsync().Result;
            }
            finally
            {
                content.Dispose();
            }
            
            return RedirectToAction("Index");
        }
    }
}
