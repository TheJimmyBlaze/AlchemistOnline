using AlchemistOnline.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Accounts
{
    public interface IIdentityService
    {
        public Account GetAccountForIdentity(IIdentity identity);
    }
}
