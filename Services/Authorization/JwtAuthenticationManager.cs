using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using net_design_pattern.Domain.Services.Authorization;

namespace net_design_pattern.Services.Authorization
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string _key;
        public JwtAuthenticationManager(string key)
        {
            _key = key;
        }
        public string Authenticate(int accountId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();// nitializes a new instance of the JwtSecurityTokenHandler
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Indentity
                Subject = new ClaimsIdentity(new Claim[] {
                    // claims is to check for authorization
                    new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
                    new Claim(ClaimTypes.Name, email),
                }),
                Expires = DateTime.UtcNow.AddHours(24), // token exprire
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
//Step 
//Create Security Handler – “token handler”.
//Once Token Handler is created, we will encrypt the key into bytes.
//Now we will create a token descriptor.
//Subject – New Claim identity
//Expired – When it will be expired.
//SigningCredentical – Private key + Algorithm
//Now we will create a token using the “create token” method of the token handler.
//Return token from Authentication method.