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
        //Dependence injection from constructor
        public IProfileService _profileService;
        public UserController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        //Get profile with user logined
        [HttpGet("profile")]
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

        //Edit profile
        [HttpPut("profile")]
        public ActionResult UpdateProfile([FromBody] ProfileDto profile)
        {
            var response = new Response<ProfileDto>();
            int accountId = 3;
            var profileRes = _profileService.EditProfile(accountId, profile);
            if(profileRes == null)
            {
                response.Code = 404; 
                response.IsSuccess = false;
                response.Message = "Profile is not exist.";
                return NotFound(response);
            }

            response.Data = profileRes;
            response.Message = "Update profile successfully.";
            return Ok(response);
        }

        //Get profile by email, for admin want to search
        [HttpGet("profile/{email}")]
        public ActionResult  GetUserProfileByEmail(string email)
        {
            var response = new Response<ProfileDto>();
            int accountId = 2; //Get from login (admmin account)
            var profileRes = _profileService.GetProfileByEmail(accountId, email);
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
    }
}