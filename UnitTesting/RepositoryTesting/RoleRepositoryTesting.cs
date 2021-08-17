using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Persistence.Context;
using net_design_pattern.Persistence.Helper;
using net_design_pattern.Persistence.Repositories.Authorization;
using Xunit;

namespace net_design_pattern.UnitTesting.RepositoryTesting
{
    public class RoleRepositoryTesting
    {
        IRoleRepository roleRepository;
        //Get account roles test
        [Fact]
        public void ProfileRepository_GetRoles_Test()
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
                context.Roles.Add(new Role
                {
                    Id = 2,
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
                 context.AccountHasRoles.Add(new AccountHasRole
                {
                    Id =2,
                    AccountId = 1,
                    RoleId = 2
                });

                context.SaveChanges();
            }
            using (var context = new AppDbContext(options))
            {
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                });
                
                var mapper = mockMapper.CreateMapper();
                roleRepository = new RoleRepository(context, mapper);
                //Act
                var result = roleRepository.GetRoles(1).ToList();
                //Assert
                Assert.NotNull(result);
                result.Should().HaveCount(2);
            }
        }
        [Fact]
        public void ProfileRepository_CheckRoles_Test()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "phonestore")
            .Options;
            using (var context = new AppDbContext(options))
            {
                var mockMapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                });
                
                var mapper = mockMapper.CreateMapper();
                roleRepository = new RoleRepository(context, mapper);
                //Act
                var result = roleRepository.CheckRole(1);
                //Assert
                result.Should().Equals(true);
            }
        }
    }
}