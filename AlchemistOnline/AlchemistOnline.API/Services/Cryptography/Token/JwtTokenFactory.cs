using AlchemistOnline.API.Services.Accounts;
using AlchemistOnline.Model.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Cryptography.Token
{
    public class JwtTokenFactory : ITokenFactory
    {
        private readonly CryptographySettings settings;

        public JwtTokenFactory(CryptographySettings settings)
        {
            this.settings = settings;
        }

        public string CreateToken(Account account)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(settings.TokenSecret);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.AccountID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(settings.TokenHoursToLive),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);
            string tokenString = handler.WriteToken(token);

            return tokenString;
        }
    }
}
