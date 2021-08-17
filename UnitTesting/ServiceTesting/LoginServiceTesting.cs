using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using net_design_pattern.Domain.Models.Authorization;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Authorization;
using net_design_pattern.Services.Authorization;
using Xunit;

namespace net_design_pattern.UnitTesting.ServiceTesting
{
    public class LoginServiceTesting
    {
        private Mock<ILoginRepository> _loginRepository = new Mock<ILoginRepository>();
        private ILoginService _loginService;
        [Fact]
        public void LoginService_Login_Test()
        {
            _loginService = new LoginService(_loginRepository.Object);  
            //Arrange
            string email = "jessie@enclave.vn";
            string password = "123";
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.Email = "jessie@enclave.vn";
            loginResponse.FullName = "Nguyen Trinh";
            _loginRepository.Setup(c => c.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(loginResponse);
            //Act
            var result = _loginService.Login(email,password);
            //Assert
            result.Email.Should().Equals(loginResponse.Email);
        }
    }
}