using net_design_pattern.Domain.Models.Authorization;

namespace net_design_pattern.Domain.Repositories.Authorization
{
    public interface ILoginRepository
    {
        LoginResponse Login(string email, string password);
    }
}