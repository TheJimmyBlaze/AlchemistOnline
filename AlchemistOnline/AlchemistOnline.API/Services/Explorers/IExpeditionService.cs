using AlchemistOnline.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Explorers
{
    public interface IExpeditionService
    {
        public Expedition BeginExpedition(int explorerID, int environmentLocationID);

        public IEnumerable<Ingredient> CompleteExpedition(int expeditionID);

        public double? ExpeditionSecondsRemaining(int expeditionID);

        public bool OwnsExpedition(IIdentity identity, int expeditionID);
    }
}
