using AlchemistOnline.ConsoleApp.Services.Accounts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AlchemistOnline.ConsoleApp.Util
{
    public class ApiClient: HttpClient
    {
        public ApiClient(IAccountService accountService)
        {
            if (accountService.Account != null && 
                !string.IsNullOrEmpty(accountService.Account.Token))
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accountService.Account.Token);
        }
    }
}
