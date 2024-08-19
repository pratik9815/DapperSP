using Microsoft.AspNetCore.Mvc.Rendering;

namespace DapperWithSQL.Models
{
    public class ImageVM
    {
        public string ImageName { get; set; }
        public IFormFile ImageData { get; set; }
        //selected image type is passed through this
        public string IdType { get; set; }
        // slect and option are rendered through this
        public List<SelectListItem> SelectStaticValueType { get; set; }  

        public string ImageType { get; set; }

    }
}
