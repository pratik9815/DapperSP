using DapperWithSQL.Models;

namespace DapperWithSQL.IRepository
{
    public interface IRegistrationRepository
    {
        void GenerateOtp(UserModel model);
    }
}
