using net_design_pattern.Domain.Models.Authorization;

namespace net_design_pattern.Domain.Services.Authorization
{
    public interface IRegisterService
    {
        bool CheckAccountExistence(string email);
        int Register(RegisterModel register);
    }
}