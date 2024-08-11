using DapperWithSQL.DataContext;
using DapperWithSQL.IRepository;
using DapperWithSQL.Models;
using DapperWithSQL.Services;

namespace DapperWithSQL.Repository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly DapperContext _context;
        private readonly IOTPService _otpService;
        public RegistrationRepository(DapperContext context,IOTPService otpService)
        {
            _context = context;
            _otpService = otpService;
        }

        public void GenerateOtp(UserModel model)
        {
            _otpService.SendOtpService(model);
        }


    }
}
