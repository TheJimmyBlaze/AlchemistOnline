using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlchemistOnline.API.Services;
using AlchemistOnline.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlchemistOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AlchemistContext context;

        public AccountController(AlchemistContext context)
        {
            this.context = context;
        }

        // GET: api/Account
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return context.Accounts;
        }

        // GET: api/Account/5
        [HttpGet("{accountID}", Name = "Get")]
        public Account Get(int accountID)
        {
            return context.Accounts.SingleOrDefault(account => account.AccountID == accountID);
        }

        // POST: api/Account
        [HttpPost]
        public void Post([FromBody] Account account)
        {
        }

        // PUT: api/Account/5
        [HttpPut("{accountID}")]
        public void Put(int accountID, [FromBody] Account account)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{accountID}")]
        public void Delete(int accountID)
        {
        }
    }
}
