using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DapperWithSQL.Services
{
    public class OTPService : IOTPService
    {
        private readonly DapperContext _context;
        public OTPService(DapperContext context)
        {
            _context = context;
        }
        public void SendOtpService(UserModel user)
        {
            string activationCode = this.GenerateOTP();

            using(var conn = _context.DbConnection())
            {
                string sql = "INSERT INTO UserOTP(userId, email, otp) VALUES (@userId, @email , @otp)";
                DynamicParameters param = new DynamicParameters();

                param.Add("@userId",user.UserId);
                param.Add("@otp",activationCode);
                param.Add("@email", user.Email);

                conn.Execute(sql, param);
            }


            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("331c34dd05d49e", "00bb6ac3cebcf0"),
                EnableSsl = true
            };


            MailMessage message = new MailMessage
            {
                From = new MailAddress("no-reply@example.com"),
                Subject = "OTP",
                IsBodyHtml = true
            };

            message.To.Add(user.Email);

            StringBuilder mailBody = new StringBuilder();

            mailBody.AppendFormat($"<h1>{activationCode} is your 5 digit OTP</h1>");
            mailBody.AppendFormat("<br>");
            mailBody.AppendFormat("<p>Thank you for registering.</p>");

            message.Body = mailBody.ToString();

            client.Send(message);
        }
        protected string GenerateOTP()
        {
            string characters = "0123456789";
            string otp = string.Empty;

            for (int i = 0; i < 5; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();

                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }
    }
}
