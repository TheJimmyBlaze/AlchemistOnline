using AlchemistOnline.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Cryptography.Token
{
    public interface ITokenFactory
    {
        public string CreateToken(Account account);
    }
}
