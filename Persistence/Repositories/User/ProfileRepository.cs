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
            throw new NotImplementedException();
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