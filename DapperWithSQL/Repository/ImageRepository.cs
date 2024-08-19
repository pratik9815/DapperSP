using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.IRepository;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        public  IEnumerable<StaticValue> GetStaticValue() 
        {
            IEnumerable<StaticValue> staticValue = new List<StaticValue>();

            using(IDbConnection conn = _dapperContext.DbConnection())
            {
                string sql = "Select *from static_values_type where description = @description";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@description", "file");
                staticValue = conn.Query<StaticValue>(sql,dynamicParameters);
            }

            return staticValue;
        }

        public async Task<Api_Response> AddImage(ImageVM image)
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
                string sql = "Insert into customerImage(Name, fileName, filePath,idType,imageType) values (@Name, @fileName, @path, @idType, @imageType)";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Name", image.ImageName);
                dynamicParameters.Add("@fileName", fileName);
                dynamicParameters.Add("@path", fileSavePath);
                dynamicParameters.Add("@idType", image.IdType);
                dynamicParameters.Add("@imageType", image.ImageType);
                await conn.ExecuteAsync(sql, dynamicParameters);
                response.Msg = "Image";
            }

            return response;
        }

        public JsonResult AjaxMethod(string sno, string value)
        {
            IEnumerable<StaticValue> staticValue = new List<StaticValue>();
            using (var conn = _dapperContext.DbConnection())
            {
                string sql = "Select *from static_values where sno = @sno and description = @description";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@sno", sno);
                dynamicParameters.Add("@description",value);
                staticValue = conn.Query<StaticValue>(sql, dynamicParameters);
            }
            return new JsonResult(staticValue);
        }
        public IEnumerable<ImageResponse> GetImage()
        {
            IEnumerable<ImageResponse> image = new List<ImageResponse>();
            using(var conn = _dapperContext.DbConnection())
            {
                string sql = "Select *from customerimage";
                image = conn.Query<ImageResponse>(sql);
            }
            return image;
        }
    }
}
