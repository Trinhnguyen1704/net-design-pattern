using System;
using System.Linq;
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
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordService _passwordService;
        public LoginRepository(AppDbContext context,
                                IJwtAuthenticationManager jWTAuthenticationManager,
                                IRoleRepository roleRepository,
                                IPasswordService passwordService)
        {
            _jwtAuthenticationManager = jWTAuthenticationManager;
            _context = context;
            _roleRepository = roleRepository;
            _passwordService = passwordService;
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
                var passwordDecode = _passwordService.PasswordDecoder(account.Password);
                if(password.Equals(passwordDecode))
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