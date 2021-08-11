namespace net_design_pattern.Domain.Services.Authorization
{
    public interface IPasswordService
    {
        string PasswordEncoder(string passwordEncode);
        string PasswordDecoder(string passwordDecode);
    }
}