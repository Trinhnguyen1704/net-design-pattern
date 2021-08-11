using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.Authorization;
using net_design_pattern.Domain.Services.Authorization;
using net_design_pattern.Domain.Services.Communication;

namespace Namespace
{
    [Route("api")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {   
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        public AuthorizationController(ILoginService loginService, IRegisterService registerService)
        {
            _loginService = loginService;
            _registerService = registerService;
        }
        /// <summary>
        /// Register new account.
        /// </summary>
        //attribute to bypass the authentication.
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterModel register)
        {
            var response = new RegisterResponse();
            bool checkAccountExistence = _registerService.CheckAccountExistence(register.Email);
            if(checkAccountExistence == true)
            {
                var message = new BaseResponse(400, "Email is existing!");
                return BadRequest(message);
            }
            int checkRegister = _registerService.Register(register);
            if(checkRegister == -1)
            {
                response.IsSuccess = false;
                return BadRequest(response);
            }
            response.AccountId = checkRegister;
            return Ok(response);
        }

        /// <summary>
        /// Login to application.
        /// </summary>
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