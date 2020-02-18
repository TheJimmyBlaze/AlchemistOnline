using AlchemistOnline.API.Services.Accounts;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.Model.Database;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Explorers
{
    public class ExplorerService : IExplorerService
    {
        private readonly AlchemistContext context;
        private readonly Random random;
        private readonly PersonNameGenerator nameGenerator;

        private readonly IIdentityService identityService;
        
        public ExplorerService(AlchemistContext context, Random random, PersonNameGenerator nameGenerator, IIdentityService identityService)
        {
            this.context = context;
            this.random = random;
            this.nameGenerator = nameGenerator;

            this.identityService = identityService;
        }

        private ExplorerType GetRandomExplorerType()
        {
            int count = context.ExplorerTypes.Count();
            int index = random.Next(0, count);

            ExplorerType type = context.ExplorerTypes.AsEnumerable().ElementAt(index);
            return type;
        }

        public Explorer GenerateExplorer()
        {
            Explorer explorer = new Explorer
            {
                Name = nameGenerator.GenerateRandomFirstAndLastName(),
                ExplorerTypeID = GetRandomExplorerType().ExplorerTypeID
            };

            return explorer;
        }

        public double CalculateLevel(double xp)
        {
            return Math.Log(0.24 * xp + 1);
        }

        public bool OwnsExplorer(IIdentity identity, int explorerID)
        {
            Explorer explorer = context.Explorers.SingleOrDefault(explorer => explorer.ExplorerID == explorerID);

            if (explorer == null)
                return false;

            Account account = identityService.GetAccountForIdentity(identity);
            if (account == null)
                return false;

            return account.AccountID == explorer.AccountID;
        }
    }
}
