using AlchemistOnline.API.Services.Context;
using AlchemistOnline.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Explorers
{
    public class ExpeditionService : IExpiditionService
    {
        private readonly AlchemistContext context;
        private readonly Random random;

        public ExpeditionService(AlchemistContext context, Random random)
        {
            this.context = context;
            this.random = random;
        }

        public Expedition BeginExpedition(int explorerID, int environmentLocationID)
        {
            Expedition expedition = new Expedition
            {
                DepartureTime = DateTime.UtcNow,
                ExplorerID = explorerID,
                EnvironmentLocationID = environmentLocationID
            };

            context.Expeditions.Add(expedition);
            context.SaveChanges();

            return expedition;
        }

        public IEnumerable<Ingredient> CompleteExpedition(int expeditionID)
        {
            Expedition expedition = context.Expeditions.SingleOrDefault(expedition => expedition.ExpeditionID == expeditionID);
            if (expedition == null)
                return null;

            EnvironmentLocation environment = context.EnvironmentLocations.Single(environment => environment.EnvironmentLocationID == expedition.EnvironmentLocationID);
            DateTime expeditionCompletionTime = expedition.DepartureTime.AddSeconds(environment.ExpeditionSeconds);

            if (DateTime.UtcNow < expeditionCompletionTime)
                return null;

            throw new NotImplementedException();
        }
    }
}
