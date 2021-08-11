using net_design_pattern.Domain.Models.DTOs;

namespace net_design_pattern.Domain.Services.User
{
    public interface IProfileService
    {
        ProfileDto GetProfile(int accountId);
        ProfileDto EditProfile(int accountId, ProfileDto profile);
        ProfileDto GetProfileByEmail(int accountId, string email);
    }
}