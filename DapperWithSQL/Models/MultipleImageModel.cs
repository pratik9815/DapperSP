namespace DapperWithSQL.Models
{
    public class MultipleImageModel
    {
        public string Value { get; set; }

        public IFormFile CustomerImage { get; set; }
        public IFormFile CitizenshipFront { get; set; }
        public IFormFile CitizenshipBack { get; set; }
        public IFormFile NationalId { get; set; }
    }
}
