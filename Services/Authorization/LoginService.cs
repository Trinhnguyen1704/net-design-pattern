using net_design_pattern.Domain.Models.Authorization;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Authorization;

namespace net_design_pattern.Services.Authorization
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public LoginResponse Login(string email, string password)
        {
            return _loginRepository.Login(email, password);
        }
    }
}