using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Authorization;
using net_design_pattern.Services.Authorization;
using Xunit;

namespace net_design_pattern.UnitTesting.ServiceTesting
{
    public class RegisterServiceTesting
    {
        private Mock<IRegisterRepository> _registerRepository = new Mock<IRegisterRepository>();
        private IRegisterService _registerService;

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RegisterService_CheckAccountExist_Test(bool checkAccount)
        {
            _registerService = new RegisterService(_registerRepository.Object);
            
            //Arrange
            var email = "jessie@encalve.vn";
            _registerRepository.Setup(c => c.CheckAccountExistence(It.IsAny<string>())).Returns(checkAccount);
            //Act
            var result = _registerService.CheckAccountExistence(email);
            //Assert
            if(checkAccount)
            {
                result.Should().Equals(true);
            }
            else
            {
                result.Should().Equals(false);
            }
        }
    }
}