using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models.Authorization;

namespace net_design_pattern.Domain.Repositories.Authorization
{
    public interface IRegisterRepository
    {
        bool CheckAccountExistence(string email);
        int Register(RegisterModel register);
    }
}