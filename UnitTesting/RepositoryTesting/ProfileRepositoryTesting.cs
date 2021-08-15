using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.User;
using net_design_pattern.Persistence.Context;
using net_design_pattern.Persistence.Repositories.User;
using Xunit;

namespace net_design_pattern.UnitTesting.RepositoryTesting
{
    public class ProfileRepositoryTesting
    {
        IProfileRepository profileRepository;
        
        //Get profile by account id test
        [Fact]
        public void ProfileRepository_GetProfileByAccountId_Test()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "phonestore")
            .Options;
            using (var context = new AppDbContext(options))
            {
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
                profileRepository = new ProfileRepository(context);
                //Act
                var result = profileRepository.GetProfile(1);
                //Assert
                Assert.NotNull(result);
                result.AccountId.Should().Equals(1);
            }
        }
        //Get profile by email test
        [Fact]
        public void ProfileRepository_GetProfileByEmail_Test()
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
                    Name = "ADMIN",
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
                profileRepository = new ProfileRepository(context);
                //Act
                var result = profileRepository.GetProfileByEmail("trinh@gmail.com");
                //Assert
                Assert.NotNull(result);
                result.AccountId.Should().Equals(1);
            }
        }
    }
}