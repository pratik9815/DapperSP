using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.IRepository;
using DapperWithSQL.Models;

namespace DapperWithSQL.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IWebHostEnvironment _environment;

        public ImageRepository(DapperContext dapperContext, IWebHostEnvironment environment)
        {
            _dapperContext = dapperContext;
            _environment = environment;
        }

        public async Task<Api_Response> AddImage(Image image)
        {
            Api_Response response = new Api_Response();
            string extension = Path.GetExtension(image.ImageData.FileName)?.ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Invalid file type. Only .jpg, .jpeg, .png are allowed.");
                
            }
            string uploadFolder = Path.Combine(_environment.WebRootPath, "Uploads");
            Random random = new Random();
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            //DOC_<Folder> _<CustomerSNO>_<RAND>.Ext
            string fileName = $"DOC_Uploads_customerSNO_{random.NextInt64(100000000000, 999999999999) + extension}";
            string fileSavePath = Path.Combine(uploadFolder, fileName);
            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await image.ImageData.CopyToAsync(stream);
            }
            using(var conn =  _dapperContext.DbConnection())
            {
                string sql = "Insert into CustomerImages(Name, FilePath)";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Name", image.Name);
                dynamicParameters.Add("@fileName", fileName);

                response = await conn.QuerySingleAsync<Api_Response>(sql, dynamicParameters);
            }
            return response;
        }
    }
}
