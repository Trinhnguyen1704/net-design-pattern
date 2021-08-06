using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Services.Communication;
using net_design_pattern.Domain.Services.User;

namespace net_design_pattern.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IProfileService _profileService;
        public UserController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("user/profile")]
        public ActionResult  GetUserProfile()
        {
            var response = new Response<ProfileDto>();
            int accountId = 3; //Get from login
            var profileRes = _profileService.GetProfile(accountId);
            if(profileRes == null)
            {
                response.Code = 404; 
                response.IsSuccess = false;
                response.Message = "Profile is not exist.";
                return NotFound(response);
            }
            response.Data = profileRes;
            response.Message = "Get profile successfully.";
            return Ok(response);
        }

        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }

        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}