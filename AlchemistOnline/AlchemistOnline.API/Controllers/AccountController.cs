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
using System.Security.Claims;
using AlchemistOnline.API.Services.Accounts;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.Model.Transfer.Accounts;

namespace AlchemistOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;
        private readonly IAccountService service;

        public AccountController(AlchemistContext context, IMapper mapper, IAccountService service)
        {
            this.context = context;
            this.mapper = mapper;
            this.service = service;
        }

        // GET: api/Account/Identity
        [HttpGet("Identity")]
        public AccountDTO Identity()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                string idString = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (int.TryParse(idString, out int accountID))
                {
                    Account account = context.Accounts.SingleOrDefault(account => account.AccountID == accountID);
                    return mapper.Map<AccountDTO>(account);
                }
            }

            return null;
        }

        // GET: api/Account
        [HttpGet]
        public IEnumerable<AccountDTO> Get()
        {
            IEnumerable<Account> accounts = context.Accounts;
            return mapper.Map<IEnumerable<AccountDTO>>(accounts);
        }

        // GET: api/Account/<ID>
        [HttpGet("{accountID}")]
        public AccountDTO Get(int accountID)
        {
            Account account = context.Accounts.SingleOrDefault(account => account.AccountID == accountID);
            return mapper.Map<AccountDTO>(account);
        }

        // POST: api/Account
        [AllowAnonymous]
        [HttpPost]
        public string Post([FromBody] NewAccountDTO request)
        {
            return service.CreateAccount(request);
        }

        // GET: api/Account/<Address>/<Phrase>
        [AllowAnonymous]
        [HttpGet("{address}/{phrase}")]
        public async Task<string> LoginAsync(string address, string phrase)
        {
            return await service.LoginAsync(address, phrase);
        }
    }
}
