using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlchemistOnline.API.Services;
using AlchemistOnline.API.Services.Cryptography.Hash;
using AlchemistOnline.API.Services.Cryptography.Token;
using AlchemistOnline.Model;
using AlchemistOnline.Model.Transfer;
using AlchemistOnline.Model.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AutoMapper;

namespace AlchemistOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AlchemistContext context;

        private readonly IMapper mapper;

        private readonly IHashFactory hashFactory;
        private readonly ITokenFactory tokenFactory;

        public AccountController(AlchemistContext context, IMapper mapper, IHashFactory hashFactory, ITokenFactory tokenFactory)
        {
            this.context = context;
            this.mapper = mapper;
            this.hashFactory = hashFactory;
            this.tokenFactory = tokenFactory;
        }

        // GET: api/Account
        [HttpGet]
        public IEnumerable<DisplayAccount> Get()
        {
            return mapper.Map<IEnumerable<DisplayAccount>>(context.Accounts);
        }

        // GET: api/Account/<ID>
        [HttpGet("{accountID}", Name = "Get")]
        public Account Get(int accountID)
        {
            return context.Accounts.SingleOrDefault(account => account.AccountID == accountID);
        }

        // POST: api/Account
        [AllowAnonymous]
        [HttpPost]
        public string Post([FromBody] NewAccount request)
        {
            if (string.IsNullOrEmpty(request.DisplayName))
                throw new ArgumentException("Display Name cannot be null");
            if (string.IsNullOrEmpty(request.Address))
                throw new ArgumentException("Address cannot be null");
            if (string.IsNullOrEmpty(request.Phrase))
                throw new ArgumentException("Phrase cannot be null");

            byte[] hash = hashFactory.BuildHash(request.Phrase);

            AccountKey key = context.AccountKeys.Add(new AccountKey { Key = hash, KeyCreationDate = DateTime.UtcNow });
            AccountEmail email = context.AccountEmails.Add(new AccountEmail { EmailAddress = request.Address });
            Account account = context.Accounts.Add(new Account
            { 
                DisplayName = request.DisplayName, 
                Email = email, 
                Key = key, 
                AccountCreationDate = DateTime.UtcNow,
                LastOnline = DateTime.UtcNow
            });

            context.SaveChanges();
            return CreateToken(account);
        }

        // GET: api/Account/<Address>/<Phrase>
        [AllowAnonymous]
        [HttpGet("{address}/{phrase}", Name = "Login")]
        public async Task<string> LoginAsync(string address, string phrase)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Display Name cannot be null");
            if (string.IsNullOrEmpty(phrase))
                throw new ArgumentException("Phrase cannot be null");

            AccountEmail accountEmail = context.AccountEmails.SingleOrDefault(email => email.EmailAddress == address);

            //If account is null, simulate hashing delay.
            if (accountEmail == null)
            {
                await hashFactory.SimulateValidateAsync();
                return null;
            }

            Account account = accountEmail.Account;
            account.LastOnline = DateTime.UtcNow;
            context.SaveChanges();

            if (account.Key == null)
                account.Key = context.AccountKeys.Single(key => key.AccountKeyID == account.AccountID);

            if (hashFactory.ValidateString(phrase, account.Key.Key))
                return CreateToken(account);

            return null;
        }

        private string CreateToken(Account account)
        {
            string jwt = tokenFactory.CreateToken(account);
            AccountToken token = context.AccountTokens.SingleOrDefault(token => token.Account.AccountID == account.AccountID);

            if (token == null)
                token = context.AccountTokens.Add(new AccountToken());

            token.Token = jwt;
            account.Token = token;
            context.SaveChanges();

            return jwt;
        }
    }
}
