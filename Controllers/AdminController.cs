using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Repositories.Authorization;

namespace net_design_pattern.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // test api get roles
        //Inject dependency from construct
        private readonly IRoleRepository _roleRepository;
        public AdminController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        [HttpGet("api/roles")]
        public ActionResult<IEnumerable<RoleDto>> GetRoles()
        {
            // accountId is get from login, but now is hard data
            var accountId = 2;
            return _roleRepository.GetRoles(accountId);
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}