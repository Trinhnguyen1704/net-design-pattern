using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using net_design_pattern.Domain.Models.Authorization;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Authorization;
using net_design_pattern.Persistence.Context;

namespace net_design_pattern.Persistence.Repositories.Authorization
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;
        private readonly IJWTAuthenticationManager _jwtAuthenticationManager;
        private readonly IRoleRepository _roleRepository;
        public LoginRepository(AppDbContext context,
                                IJWTAuthenticationManager jWTAuthenticationManager,
                                IRoleRepository roleRepository)
        {
            _jwtAuthenticationManager = jWTAuthenticationManager;
            _context = context;
            _roleRepository = roleRepository;
        }
        public LoginResponse Login(string email, string password)
        {
            try
            {
                if(email.Equals("") || password.Equals(""))
                {
                    return null;
                }
                var account = _context.Accounts
                .Include(x => x.Profile)
                .FirstOrDefault(x => x.Email.Equals(email));
                if(account == null)
                {
                    return null;
                }
                //Decode password in db and compare when use password encode
                if(password.Equals(account.Password))
                {
                    var roles = _roleRepository.GetRoles(account.Id); 
                    var returnedAccount = new LoginResponse
                    {
                        Id = account.Id,
                        FullName = account.Profile.FirstName + account.Profile.LastName,
                        Email = email,
                        Token = _jwtAuthenticationManager.Authenticate(account.Id, account.Email),
                        Roles = roles
                    };
                    return returnedAccount;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}