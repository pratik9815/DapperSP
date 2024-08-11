using DapperWithSQL.Models;

namespace DapperWithSQL.Services
{
    public interface IOTPService
    {
        void SendOtpService(UserModel user);
    }
}
