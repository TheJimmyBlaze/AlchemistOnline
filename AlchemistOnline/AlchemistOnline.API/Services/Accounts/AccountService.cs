using AlchemistOnline.API.Exceptions;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.API.Services.Cryptography.Hash;
using AlchemistOnline.API.Services.Cryptography.Token;
using AlchemistOnline.API.Services.Explorers;
using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer;
using AlchemistOnline.Model.Transfer.Accounts;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Accounts
{
    public class AccountService: IAccountService
    {
        private readonly AlchemistContext context;
    
        private readonly IHashFactory hashFactory;
        private readonly ITokenFactory tokenFactory;

        private readonly IExplorerService explorerService;

        public AccountService(AlchemistContext context, IHashFactory hashFactory, ITokenFactory tokenFactory, IExplorerService explorerService)
        {
            this.context = context;
            this.hashFactory = hashFactory;
            this.tokenFactory = tokenFactory;
            this.explorerService = explorerService;
        }

        public bool AddressAvailable(string address)
        {
            return context.AccountEmails.Count(email => email.EmailAddress == address) == 0;
        }

        public bool DisplayNameAvailable(string displayName)
        {
            return context.Accounts.Count(account => account.DisplayName == displayName) == 0;
        }

        public string CreateAccount(NewAccountDTO request)
        {
            if (string.IsNullOrEmpty(request.DisplayName))
                throw new InvalidAccountCredentialsException("Display Name cannot be null");
            if (string.IsNullOrEmpty(request.Address))
                throw new InvalidAccountCredentialsException("Address cannot be null");
            if (string.IsNullOrEmpty(request.Phrase))
                throw new InvalidAccountCredentialsException("Phrase cannot be null");

            byte[] hash = hashFactory.BuildHash(request.Phrase);

            AccountKey key = new AccountKey { Key = hash, KeyCreationDate = DateTime.UtcNow };
            AccountEmail email = new AccountEmail { EmailAddress = request.Address.ToLower() };
            Account account = new Account
            {
                DisplayName = request.DisplayName,
                AccountEmail = email,
                AccountKey = key,
                AccountCreationDate = DateTime.UtcNow,
                LastOnline = DateTime.UtcNow
            };

            SetupAccount(account);

            context.Add(account);
            context.SaveChanges();

            return tokenFactory.CreateToken(account);
        }

        private void SetupAccount(Account account)
        {
            Explorer explorer = explorerService.GenerateExplorer();

            if (account.Explorers == null)
                account.Explorers = new List<Explorer>();

            account.Explorers.Add(explorer);
        }

        public async Task<string> LoginAsync(string address, string phrase)
        {
            if (string.IsNullOrEmpty(address))
                throw new InvalidAccountCredentialsException("Display Name cannot be null");
            if (string.IsNullOrEmpty(phrase))
                throw new InvalidAccountCredentialsException("Phrase cannot be null");

            AccountEmail accountEmail = context.AccountEmails.SingleOrDefault(email => email.EmailAddress == address.ToLower());

            //If account is null, simulate hashing delay.
            if (accountEmail == null)
            {
                await hashFactory.SimulateValidateAsync();
                return null;
            }

            context.Entry(accountEmail).Reference(email => email.Account).Load();
            Account account = accountEmail.Account;
            account.LastOnline = DateTime.UtcNow;
            context.SaveChanges();

            context.Entry(account).Reference(account => account.AccountKey).Load();
            if (hashFactory.ValidateString(phrase, account.AccountKey.Key))
                return tokenFactory.CreateToken(account);

            return null;
        }
    }
}
