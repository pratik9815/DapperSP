using System.ComponentModel.DataAnnotations;

namespace DapperWithSQL.Models
{
    public class UniversityInformation
    {
        [Required(ErrorMessage = "The college id is required.")]
        public string CollegeId { get; set; }
        [Required(ErrorMessage = "The college name is required.")]
        public string CollegeName { get; set; }
        [Required(ErrorMessage = "The college address is required.")]
        public string CollegeAddress { get; set; }
        [Required(ErrorMessage = "The college contact is required.")]

        public string CollegeContact { get; set; }
    }
    public class UniversityInformations
    {
        public List<UniversityInformation> UniversityInfoList { get; set; }
    }
}
