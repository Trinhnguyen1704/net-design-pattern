using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Services.Communication;
using net_design_pattern.Domain.Services.User;

namespace net_design_pattern.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Get user profile.
        /// </summary>
        //Get profile with user logined
        [HttpGet("profile")]
        public ActionResult  GetUserProfile()
        {
            var response = new Response<ProfileDto>();
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var profileRes = _profileService.GetProfile(Int32.Parse(accountId));
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

        /// <summary>
        /// Update profile.
        /// </summary>
        //Edit profile
        [HttpPut("profile")]
        public ActionResult UpdateProfile([FromBody] ProfileDto profile)
        {
            var response = new Response<ProfileDto>();
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var profileRes = _profileService.EditProfile(Int32.Parse(accountId), profile);
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

        /// <summary>
        /// Get profile by email.
        /// </summary>
        //Get profile by email, for admin want to search
        [HttpGet("profile/{email}")]
        public ActionResult  GetUserProfileByEmail(string email)
        {
            var response = new Response<ProfileDto>();
             var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var profileRes = _profileService.GetProfileByEmail(Int32.Parse(accountId), email);
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