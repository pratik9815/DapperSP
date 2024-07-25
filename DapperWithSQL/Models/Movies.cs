using System.ComponentModel.DataAnnotations;

namespace DapperWithSQL.Models
{
    public class Movies
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Movie name must be atleast 3 characters long")]
        [Required(ErrorMessage = "Movie name is required")]
        public string Movie_Name { get; set; }

        [StringLength(60, MinimumLength = 3, ErrorMessage = "Movie name confirmation must be atleast 3 characters long")]
        [Required(ErrorMessage = "Movie name confirmation is required")]
        [Compare("Movie_Name", ErrorMessage = "The above movie name does not match")]
        public string Movie_Name_Confirmation { get; set; }

        [Range(1, 1000, ErrorMessage = "Price must be more than $1")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Price is required")]
        public decimal Movie_Price { get; set; }
    }
}
