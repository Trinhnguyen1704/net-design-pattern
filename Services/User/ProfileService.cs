using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Services.User;

namespace net_design_pattern.Services.User
{
    public class ProfileService : IProfileService
    {
        public ProductDto EditProfile(int accountId, ProfileDto profile)
        {
            throw new NotImplementedException();
        }

        public ProfileDto GetProfile(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}