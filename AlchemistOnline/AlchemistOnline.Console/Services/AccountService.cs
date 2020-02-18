using AlchemistOnline.Model.Display;
using AlchemistOnline.Model.Transfer.Accounts;
using AutoMapper;
using Flurl;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AlchemistOnline.ConsoleApp.Services
{
    public class AccountService: IAccountService
    {
        public const string ACCOUNT_CONTROLLER = "api/Account";

        public Account Account { get; private set; }

        private readonly IMapper mapper;
        private readonly JsonSerializerOptions jsonOptions;

        public AccountService(IMapper mapper, JsonSerializerOptions jsonOptions)
        {
            this.mapper = mapper;
            this.jsonOptions = jsonOptions;
        }

        public async Task<bool> EmailAvailable(string address)
        {
            using HttpClient client = new HttpClient();

            string url = Url.Combine(Program.ApiAddress,
                                    ACCOUNT_CONTROLLER,
                                    "Available/Address",
                                    address);

            string result = await client.GetStringAsync(url);
            if (bool.TryParse(result, out bool available))
                return available;
            return false;
        }

        public async Task<bool> DisplayNameAvailable(string displayName)
        {
            using HttpClient client = new HttpClient();

            string url = Url.Combine(Program.ApiAddress,
                                    ACCOUNT_CONTROLLER,
                                    "Available/DisplayName",
                                    displayName);

            string result = await client.GetStringAsync(url);
            if (bool.TryParse(result, out bool available))
                return available;
            return false;
        }

        public async Task<string> RequestToken(string address, string phrase)
        {
            using HttpClient client = new HttpClient();

            string url = Url.Combine(Program.ApiAddress,
                                    ACCOUNT_CONTROLLER,
                                    address,
                                    phrase);


            string token = await client.GetStringAsync(url);
            return token;
        }

        public async Task<string> RequestNewAccountToken(string displayName, string address, string phrase)
        {
            using HttpClient client = new HttpClient();

            string url = Url.Combine(Program.ApiAddress,
                                    ACCOUNT_CONTROLLER);

            NewAccountDTO request = new NewAccountDTO()
            {
                DisplayName = displayName,
                Address = address,
                Phrase = phrase
            };

            HttpContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                return token;
            }

            return null;
        }

        public async Task<Account> GetAccount(string token)
        {
            using HttpClient client = new HttpClient();

            string url = Url.Combine(Program.ApiAddress,
                                    ACCOUNT_CONTROLLER,
                                    "Identity");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string json = await client.GetStringAsync(url);

            AccountDTO dto = JsonSerializer.Deserialize<AccountDTO>(json, jsonOptions);
            Account = mapper.Map<Account>(dto);
            Account.Token = token;

            return Account;
        }
    }
}
