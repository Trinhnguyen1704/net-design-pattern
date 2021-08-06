using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Repositories.User;
using net_design_pattern.Domain.Services.User;

namespace net_design_pattern.Services.User
{
    public class ProfileService : IProfileService
    {
        public IProfileRepository _profileRepository;
        public IMapper _mapper;
        public ProfileService(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }
        public ProductDto EditProfile(int accountId, ProfileDto profile)
        {
            throw new NotImplementedException();
        }

        public ProfileDto GetProfile(int accountId)
        {
            var profileRes = _profileRepository.GetProfile(accountId);
            if(profileRes == null)
            {
                return null;
            }
            return _mapper.Map<ProfileDto>(profileRes);
        }
    }
}