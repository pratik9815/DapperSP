using System.ComponentModel.DataAnnotations;

namespace DapperWithSQL.Models
{
    public class Login
    {
        //public int LoginId { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
