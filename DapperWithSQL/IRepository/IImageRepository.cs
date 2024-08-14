using DapperWithSQL.Models;

namespace DapperWithSQL.IRepository
{
    public interface IImageRepository
    {
       Task<Api_Response> AddImage(Image image);
    }
}
