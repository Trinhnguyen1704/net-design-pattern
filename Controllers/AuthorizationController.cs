using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.Authorization;
using net_design_pattern.Domain.Services.Authorization;
using net_design_pattern.Domain.Services.Communication;

namespace Namespace
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {   
        private readonly ILoginService _loginService;
        public AuthorizationController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        //attribute to bypass the authentication.
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel loginModel)
        {
            var account = _loginService.Login(loginModel.Email, loginModel.Password);
            var response = new Response<LoginResponse>(account);
            if (account != null)
            {
                response.Message = "Login Successfully.";
                return Ok(response);
            }
            response.IsSuccess = false;
            response.Message = "Email or password is incorrect.";
            response.Code = 400;
            return BadRequest(response);
        }
    }
}