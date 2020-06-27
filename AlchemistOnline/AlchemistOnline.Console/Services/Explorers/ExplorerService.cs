using AlchemistOnline.ConsoleApp.Services.Accounts;
using AlchemistOnline.ConsoleApp.Util;
using Flurl;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlchemistOnline.ConsoleApp.Services.Explorers
{
    public class ExplorerService : IExplorerService
    {
        public const string EXPLORER_CONTROLLER = "api/Explorer";

        private readonly IAccountService accountService;

        public ExplorerService(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<int> ExplorerCount()
        {
            using ApiClient client = new ApiClient(accountService);

            string url = Url.Combine(Program.ApiAddress,
                                    EXPLORER_CONTROLLER,
                                    "Account/Count",
                                    accountService.Account.AccountID.ToString());

            string result = await client.GetStringAsync(url);
            if (int.TryParse(result, out int count))
                return count;
            return -1;
        }

        public async Task<int> IdleExplorerCount()
        {
            using ApiClient client = new ApiClient(accountService);

            string url = Url.Combine(Program.ApiAddress,
                                    EXPLORER_CONTROLLER,
                                    "Account/IdleCount",
                                    accountService.Account.AccountID.ToString());

            string result = await client.GetStringAsync(url);
            if (int.TryParse(result, out int count))
                return count;
            return -1;
        }
    }
}
