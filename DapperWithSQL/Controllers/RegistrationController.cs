using Azure.Core;
using DapperWithSQL.IRepository;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DapperWithSQL.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IConfiguration _configuration;

        public RegistrationController(IRegistrationRepository registrationRepository, IConfiguration configuration)
        {
            _registrationRepository = registrationRepository;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            //_registrationRepository.GenerateOtp(model);
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {
            if (model.UserName == "pratik" && model.Password == "Pratik@123")
            {
                var token = GenerateAccessToke(model);
                return Ok(new { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            ViewBag.Message = "Please enter correct credentials";
            return Unauthorized("Invalid Credentials");
        }

        private JwtSecurityToken GenerateAccessToke(Login model)
        {
            //creating user claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name ,model.UserName)
                //we can add additional claims as needed
            };
            //creating jwt token
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60), //Token Expiration time
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                                            SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
