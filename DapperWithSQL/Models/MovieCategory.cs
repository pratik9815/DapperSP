using System.ComponentModel.DataAnnotations;

namespace DapperWithSQL.Models
{
    public class MovieCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Genre is a required field")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Description is a required field")]
        public string Description { get; set; }
    }
}
