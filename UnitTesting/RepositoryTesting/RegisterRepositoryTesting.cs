using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Models.Authorization;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Authorization;
using net_design_pattern.Persistence.Context;
using net_design_pattern.Persistence.Repositories.Authorization;
using Xunit;

namespace net_design_pattern.UnitTesting.RepositoryTesting
{
    public class RegisterRepositoryTesting
    {
        IRegisterRepository registerRepository;
        private Mock<IPasswordService> _passwordService = new Mock<IPasswordService>();

         //Register
        [Fact]
        public void RegisterRepository_Register_Test()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "phonestore")
            .Options;
            using (var context = new AppDbContext(options))
            {
                context.Roles.Add(new Role
                {
                    Id = 1,
                    Name = "USER",
                });
                context.Accounts.Add(new Account
                {
                    Id = 1,
                    Email = "trinh@gmail.com",
                    Password = "123",
                });
                context.AccountHasRoles.Add(new AccountHasRole
                {
                    Id =1,
                    AccountId = 1,
                    RoleId = 1
                });
                context.Profiles.Add(new Profile
                {
                    Id = 2,
                    AccountId = 1,
                    LastName = "Trinh",
                    FirstName = "Nguyen",
                    Address = "Quang Ngai",
                    PhoneNumber = "0339934148",
                    Gender = 0,
                    DateOfBirth = DateTime.Parse("1999-04-17")
                });
                context.SaveChanges();
            }
            using (var context = new AppDbContext(options))
            {
                registerRepository = new RegisterRepository(_passwordService.Object, context);
                _passwordService.Setup(c=> c.PasswordEncoder(It.IsAny<string>())).Returns("123qwe!@#");
                RegisterModel register = new RegisterModel();
                register.Email = "jessie@enclave.vn";
                register.FirstName = "Nguyen";
                register.LastName = "Trinh";
                register.Password = "123qwe!@#";
                //Act
                var result = registerRepository.Register(register);
                //Assert
                Assert.NotNull(result);
                result.Should().Equals(2);
            }
        }
        [Theory]
        [InlineData("trinh@gmail.com")]
        [InlineData("trinh123@gmail.com")]
        public void RegisterRepository_CheckRole_Test(string email)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "phonestore")
            .Options;
             using (var context = new AppDbContext(options))
            {
                registerRepository = new RegisterRepository(_passwordService.Object, context);
                //Act
                var result = registerRepository.CheckAccountExistence(email);
                //Assert
                if(email =="trinh@gmail.com")
                {
                    result.Should().Equals(true);
                }
                if(email == "trinh123@gmail.com")
                {
                    result.Should().Equals(false);
                }
            }
        }
    }
}