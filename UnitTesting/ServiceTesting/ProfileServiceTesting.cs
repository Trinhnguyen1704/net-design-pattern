using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Repositories.User;
using net_design_pattern.Domain.Services.User;
using net_design_pattern.Persistence.Helper;
using net_design_pattern.Services.User;
using Xunit;

namespace net_design_pattern.UnitTesting.ServiceTesting
{
    public class ProfileServiceTesting
    {
        private Mock<IProfileRepository> _profileRepository = new Mock<IProfileRepository>();
        private Mock<IRoleRepository> _roleRepository = new Mock<IRoleRepository>();
        private IProfileService _profileService;
        private List<Domain.Models.Profile> _listProduct = new List<Domain.Models.Profile>()
        {
            new Domain.Models.Profile() {Id =1, FirstName= "Nguyen", LastName = "Trinh", AccountId = 1, PhoneNumber = "0339934123", Gender = 0, DateOfBirth = DateTime.Parse("1999-04-17"), Address="Da Nang"},
            new Domain.Models.Profile() {Id =2, FirstName= "Tran", LastName = "An", AccountId = 2, PhoneNumber = "0339934123", Gender = 1, DateOfBirth = DateTime.Parse("1999-12-21"), Address="Quang Ngai"},
            new Domain.Models.Profile() {Id =3, FirstName= "Nguyen", LastName = "Van", AccountId = 3, PhoneNumber = "0339934123", Gender = 1, DateOfBirth = DateTime.Parse("1999-08-27"), Address="Hue"},
        };
        
        [Fact]
        public void ProductService_UpdateItem_Test()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            int accountId = 1;
            var mapper = mockMapper.CreateMapper();
            _profileService = new ProfileService(_profileRepository.Object,mapper,_roleRepository.Object);

            ProfileDto profile = new ProfileDto();
            int id = 1;
            profile.AccountId = 1;
            profile.LastName = "Jessie";
            profile.FirstName = "Nguyen";
            profile.Address = "Quang Ngai";
            profile.PhoneNumber = "0339934148";
            profile.Gender = "Female";
            profile.DateOfBirth = DateTime.Parse("1999-04-9");

            _profileRepository.Setup(c => c.EditProfile(It.IsAny<int>(), It.IsAny<Domain.Models.Profile>())).Returns((int profileId, Domain.Models.Profile res) =>
            {
                profileId = id;
                res.AccountId = id;
                return res;
            });
            _roleRepository.Setup(r => r.CheckRole(accountId)).Returns(true);
            var result = _profileService.EditProfile(accountId,profile);
            //Assert
            Assert.NotNull(result);
            result.AccountId.Should().Equals(id);
        }
        
    }
}