using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.User;

namespace net_design_pattern.Persistence.Repositories.User
{
    public class ProfileRepository : IProfileRepository
    {
        public Profile EditProfile(int accountId, Profile profile)
        {
            throw new NotImplementedException();
        }

        public Profile GetProfile(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}