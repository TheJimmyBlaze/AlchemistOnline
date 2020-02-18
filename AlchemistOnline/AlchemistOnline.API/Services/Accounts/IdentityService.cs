using AlchemistOnline.API.Services.Context;
using AlchemistOnline.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Accounts
{
    public class IdentityService : IIdentityService
    {
        private readonly AlchemistContext context;

        public IdentityService(AlchemistContext context)
        {
            this.context = context;
        }

        public Account GetAccountForIdentity(IIdentity identity)
        {
            if (identity is ClaimsIdentity claim)
            {
                string idString = claim.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (int.TryParse(idString, out int accountID))
                {
                    Account account = context.Accounts.SingleOrDefault(account => account.AccountID == accountID);
                    return account;
                }
            }

            return null;
        }
    }
}
