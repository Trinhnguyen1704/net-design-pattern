namespace net_design_pattern.Domain.Services.Authorization
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(int accountId, string email);
    }
}