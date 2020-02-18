using AlchemistOnline.Model.Display;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlchemistOnline.ConsoleApp.Services
{
    public interface IAccountService
    {
        public Task<bool> EmailAvailable(string address);

        public Task<bool> DisplayNameAvailable(string displayName);

        public Task<string> RequestToken(string address, string phrase);

        public Task<string> RequestNewAccountToken(string displayName, string address, string phrase);

        public Task<Account> GetAccount(string token);
    }
}
