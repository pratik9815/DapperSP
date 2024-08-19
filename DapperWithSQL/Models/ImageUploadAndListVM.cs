namespace DapperWithSQL.Models
{
    public class ImageUploadAndListVM
    {
        public ImageUploadAndListVM() 
        {
            Images = new List<ImageResponse>();
            imageVM = new ImageVM();
        }
        public IEnumerable<ImageResponse> Images { get; set; }
        public ImageVM imageVM { get; set; }
    }
}
