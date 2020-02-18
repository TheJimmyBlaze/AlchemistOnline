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
using AlchemistOnline.API.Exceptions;

namespace AlchemistOnline.API.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;

        private readonly IAccountService accountService;
        private readonly IIdentityService identityService;

        public AccountController(AlchemistContext context, IMapper mapper, IAccountService accountService, IIdentityService identityService)
        {
            this.context = context;
            this.mapper = mapper;

            this.accountService = accountService;
            this.identityService = identityService;
        }

        // GET: api/Account/Available/Address/<Address>
        [AllowAnonymous]
        [HttpGet("Available/Address/{address}")]
        public IActionResult AddressAvailable(string address)
        {
            return Ok(accountService.AddressAvailable(address));
        }

        // GET: api/Account/Available/DisplayName/<DisplayName>
        [AllowAnonymous]
        [HttpGet("Available/DisplayName/{displayName}")]
        public IActionResult DisplayNameAvailable(string displayName)
        {
            return Ok(accountService.DisplayNameAvailable(displayName));
        }

        // GET: api/Account/Identity
        [HttpGet("Identity")]
        public IActionResult Identity()
        {
            Account account = identityService.GetAccountForIdentity(HttpContext.User.Identity);
            if (account == null)
                return NotFound();

            return Ok(mapper.Map<AccountDTO>(account));
        }

        // GET: api/Account
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Account> accounts = context.Accounts;
            return Ok(mapper.Map<IEnumerable<AccountDTO>>(accounts));
        }

        // GET: api/Account/<ID>
        [HttpGet("{accountID}")]
        public IActionResult Get(int accountID)
        {
            Account account = context.Accounts.SingleOrDefault(account => account.AccountID == accountID);

            if (account == null)
                return NotFound();

            return Ok(mapper.Map<AccountDTO>(account));
        }

        // POST: api/Account
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] NewAccountDTO request)
        {
            try
            {
                return Ok(accountService.CreateAccount(request));
            }
            catch (InvalidAccountCredentialsException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Account/<Address>/<Phrase>
        [AllowAnonymous]
        [HttpGet("{address}/{phrase}")]
        public async Task<IActionResult> LoginAsync(string address, string phrase)
        {
            try
            {
                string jwtToken = await accountService.LoginAsync(address, phrase);

                if (string.IsNullOrEmpty(jwtToken))
                    return Unauthorized();

                return Ok(jwtToken);
            }
            catch (InvalidAccountCredentialsException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
