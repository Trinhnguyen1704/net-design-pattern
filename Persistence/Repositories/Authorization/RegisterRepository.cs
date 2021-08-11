using System;
using System.Linq;
using net_design_pattern.Domain.Models.Authorization;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Authorization;
using net_design_pattern.Persistence.Context;

namespace net_design_pattern.Persistence.Repositories.Authorization
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly IPasswordService _passwordService;
        private readonly AppDbContext _context;
        public RegisterRepository(IPasswordService passwordService, AppDbContext context)
        {
            _passwordService = passwordService;
            _context = context;
        }
        public bool CheckAccountExistence(string email)
        {
            var accountToCheck = _context.Accounts.FirstOrDefault(x => x.Email.Equals(email));
            if(accountToCheck != null)
            {
                return true;
            }
            return false;
        }

        public int Register(RegisterModel register)
        {
            try
            {
                string passwordEncode = _passwordService.PasswordEncoder(register.Password);
                var registerAccount = new Account
                {
                    Email = register.Email,
                    Password = passwordEncode
                };
                _context.Accounts.Add(registerAccount);
                _context.SaveChanges();
                
                var account = _context.Accounts.FirstOrDefault(x => x.Email == register.Email);
                var profile = _context.Profiles.FirstOrDefault(x => x.AccountId == account.Id);
                profile.FirstName = register.FirstName;
                profile.LastName = register.LastName;
                _context.SaveChanges();

                var accountHasRole = new AccountHasRole();
                var role = _context.Roles.FirstOrDefault(x => x.Name.Equals("USER"));
                accountHasRole.RoleId = role.Id;
                accountHasRole.AccountId = account.Id;
                _context.AccountHasRoles.Add(accountHasRole);
                _context.SaveChanges();

                return account.Id;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
        }
    }
}