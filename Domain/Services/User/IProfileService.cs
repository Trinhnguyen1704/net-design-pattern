using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models.DTOs;

namespace net_design_pattern.Domain.Services.User
{
    public interface IProfileService
    {
        ProfileDto GetProfile(int accountId);
        ProductDto EditProfile(int accountId, ProfileDto profile);
    }
}