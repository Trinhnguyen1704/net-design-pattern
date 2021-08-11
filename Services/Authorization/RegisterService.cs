using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models.Authorization;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Authorization;

namespace net_design_pattern.Services.Authorization
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository _registerRepository;
        public RegisterService(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }
        public bool CheckAccountExistence(string email)
        {
            return _registerRepository.CheckAccountExistence(email);
        }

        public int Register(RegisterModel register)
        {
            return _registerRepository.Register(register);
        }
    }
}