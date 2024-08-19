using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperWithSQL.IRepository
{
    public interface IImageRepository
    {
        IEnumerable<StaticValue> GetStaticValue();
       Task<Api_Response> AddImage(ImageVM image);
        JsonResult AjaxMethod(string type, string value);
        IEnumerable<ImageResponse> GetImage();
    }
}
