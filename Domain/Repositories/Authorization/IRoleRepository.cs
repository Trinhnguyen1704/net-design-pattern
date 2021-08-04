using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models.DTOs;

namespace net_design_pattern.Domain.Repositories.Authorization
{
    public interface IRoleRepository
    {
        List<RoleDto> GetRoles(int accountId);
        bool CheckRole(int accountId);
    }
}
//put abstract, representative classes in one place, I put it in Domain 
//service is like a proxy pattern design, it is the intermediary between the controller and the repository,
// confirming whether the controller has access permission to the repo or not.