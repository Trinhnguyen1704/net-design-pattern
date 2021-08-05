using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models;

namespace net_design_pattern.Domain.Repositories.User
{
    public interface IProfileRepository
    {
        Profile GetProfile(int accountId);
        Profile EditProfile(int accountId, Profile profile);
    }
}