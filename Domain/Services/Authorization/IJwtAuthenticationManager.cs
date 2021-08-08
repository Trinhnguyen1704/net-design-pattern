namespace net_design_pattern.Domain.Services.Authorization
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(int accountId, string email);
    }
}