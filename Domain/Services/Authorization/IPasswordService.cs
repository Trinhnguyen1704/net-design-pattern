using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_design_pattern.Domain.Services.Authorization
{
    public interface IPasswordService
    {
        string PasswordEncoder(string passwordEncode);
        string PasswordDecoder(string passwordDecode);
    }
}