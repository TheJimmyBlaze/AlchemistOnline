using AlchemistOnline.ConsoleApp.Services;
using AlchemistOnline.ConsoleApp.Util;
using AlchemistOnline.Model.Display;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AlchemistOnline.ConsoleApp.Commands
{
    public class LoginHandler: ILoginHandler
    {
        private readonly ConsoleInput utilInput;
        private readonly IAccountService accountService;

        public LoginHandler(ConsoleInput utilInput, IAccountService accountService)
        {
            this.utilInput = utilInput;
            this.accountService = accountService;
        }

        public async Task Login()
        {
            Console.WriteLine();
            Console.WriteLine("1: Login as existing user");
            Console.WriteLine("2: Create new account");

            string token = string.Empty;

            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    token = await LoginExisting();
                    break;
                case "2":
                    token = await CreateNew();
                    break;
                default:
                    Console.WriteLine("Option: {0} not recognized", option);
                    break;
            }

            if (string.IsNullOrEmpty(token))
            {
                await Login();
                return;
            }

            Account account = await accountService.GetAccount(token);
            Console.WriteLine("Welcome: {0}", account.DisplayName);
        }

        private async Task<string> LoginExisting()
        {
            string address = await GetAddress(false);
            string phrase = GetPhrase(false);

            try
            {
                string token = await accountService.RequestToken(address, phrase);
                return token;
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Invalid username or password");
                return null;
            }
        }

        private async Task<string> CreateNew()
        {
            string displayName = await GetDisplayName();
            string address = await GetAddress(true);
            string phrase = GetPhrase(true);

            string token = await accountService.RequestNewAccountToken(displayName, address, phrase);
            return token;
        }

        private async Task<string> GetDisplayName()
        {
            Console.WriteLine("Display name:");
            string displayName = Console.ReadLine();

            if (await accountService.DisplayNameAvailable(displayName))
                return displayName;

            Console.WriteLine("That display name is not available, try again");
            return await GetDisplayName();
        }

        private async Task<string> GetAddress(bool verifyAvailable)
        {
            Console.WriteLine("Email address:");
            string address = Console.ReadLine();

            try
            {
                MailAddress formedAddress = new MailAddress(address);
                if (formedAddress.Address == address)
                {
                    if (!verifyAvailable || await accountService.EmailAvailable(address))
                        return address;
                    else
                    {
                        Console.WriteLine("That email address is not available, try again");
                        return await GetAddress(verifyAvailable);
                    }
                }
            }
            catch (FormatException) { }

            Console.WriteLine("Invalid email address, try again");
            return await GetAddress(verifyAvailable);
        }

        private string GetPhrase(bool verifyMatch, bool printMessage = true)
        {
            if(printMessage)
                Console.WriteLine("Enter password:");
            string phrase = utilInput.GetHiddenConsoleInput();

            if (verifyMatch)
            {
                Console.WriteLine("Verify password:");
                string verify = GetPhrase(false, false);

                if (phrase == verify)
                    return phrase;
                else
                {
                    Console.WriteLine("Passwords do not match, try again");
                    GetPhrase(verifyMatch);
                }
            }

            return phrase;
        }
    }
}
