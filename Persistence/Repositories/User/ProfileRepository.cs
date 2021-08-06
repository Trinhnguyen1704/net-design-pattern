using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.User;
using net_design_pattern.Persistence.Context;

namespace net_design_pattern.Persistence.Repositories.User
{
    public class ProfileRepository : IProfileRepository
    {
        public readonly AppDbContext _context;
        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }
        public Profile EditProfile(int accountId, Profile profile)
        {
            try
            {
                var profileToUpdate = _context.Profiles.FirstOrDefault(x => x.AccountId == accountId);
                if(profileToUpdate == null)
                {
                    return null;
                }
                
                profileToUpdate.FirstName = profile.FirstName;
                profileToUpdate.LastName = profile.LastName;
                profileToUpdate.PhoneNumber = profile.PhoneNumber;
                profileToUpdate.Gender = profile.Gender;
                profileToUpdate.DateOfBirth = profile.DateOfBirth;
                profileToUpdate.Address = profile.Address;

                _context.SaveChanges();
                return profile;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Profile GetProfile(int accountId)
        {
            try
            {
                return _context.Profiles.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.AccountId == accountId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}