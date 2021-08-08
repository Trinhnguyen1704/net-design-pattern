using net_design_pattern.Domain.Models.Authorization;

namespace net_design_pattern.Domain.Services.Authorization
{
    public interface ILoginService
    {
        LoginResponse Login(string email, string password);
    }
}