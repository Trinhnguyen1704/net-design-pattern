using System.Collections.Generic;
using net_design_pattern.Domain.Models.DTOs;

namespace net_design_pattern.Domain.Models.Authorization
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string FullName { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}