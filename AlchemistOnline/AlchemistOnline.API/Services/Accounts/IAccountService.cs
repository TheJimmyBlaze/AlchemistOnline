using AlchemistOnline.Model.Transfer;
using AlchemistOnline.Model.Transfer.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Accounts
{
    public interface IAccountService
    {
        public string CreateAccount(NewAccountDTO request);

        public Task<string> LoginAsync(string address, string phrase);
    }
}
