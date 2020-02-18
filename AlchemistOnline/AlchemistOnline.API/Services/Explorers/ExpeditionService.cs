using AlchemistOnline.API.Exceptions;
using AlchemistOnline.API.Mappings;
using AlchemistOnline.API.Services.Accounts;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.API.Services.Ingredients;
using AlchemistOnline.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Explorers
{
    public class ExpeditionService : IExpeditionService
    {
        private readonly AlchemistContext context;

        private readonly IIngredientService ingredientService;
        private readonly IIdentityService identityService;
        private readonly IExplorerService explorerService;

        public ExpeditionService(AlchemistContext context, IIngredientService ingredientService, IIdentityService identityService, IExplorerService explorerService)
        {
            this.context = context;
            this.ingredientService = ingredientService;

            this.identityService = identityService;
            this.explorerService = explorerService;
        }

        public Expedition BeginExpedition(int explorerID, int environmentLocationID)
        {
            if (!CanExploreEnvironment(explorerID, environmentLocationID))
                throw new ExplorerLevelException("Explorer is not high enough level to explore this Environment");

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

        private bool CanExploreEnvironment(int explorerID, int environmentLocationID)
        {
            Explorer explorer = context.Explorers.SingleOrDefault(explorer => explorer.ExplorerID == explorerID);
            if (explorer == null)
                return false;

            context.Entry(explorer).Reference(explorer => explorer.Expedition).Load();
            if (explorer.Expedition != null)
                return false;

            EnvironmentLocation environment = context.EnvironmentLocations.SingleOrDefault(environment => environment.EnvironmentLocationID == environmentLocationID);
            if (environment == null)
                return false;

            context.Entry(environment).Reference(environment => environment.EnvironmentDifficulty).Load();

            double explorerLevel = explorerService.CalculateLevel(explorer.ExperiencePoints);
            return  explorerLevel >= environment.EnvironmentDifficulty.SkillRequirement;
        }

        public IEnumerable<Ingredient> CompleteExpedition(int expeditionID)
        {
            Expedition expedition = context.Expeditions.SingleOrDefault(expedition => expedition.ExpeditionID == expeditionID);
            if (expedition == null)
                throw new ExpeditionNotFoundException();

            context.Entry(expedition).Reference(expedition => expedition.EnvironmentLocation).Load();
            DateTime expeditionCompletionTime = expedition.DepartureTime.AddSeconds(expedition.EnvironmentLocation.ExpeditionSeconds);

            if (DateTime.UtcNow < expeditionCompletionTime)
                throw new ExpeditionInProgressException("Expedition has not yet finished");

            IEnumerable<Ingredient> collectedIngredients = ingredientService.GenerateExpeditionReward(expedition);

            context.Entry(expedition).Reference(expedition => expedition.Explorer).Load();
            ingredientService.CommitIngredientsToAccount((int)expedition.Explorer.AccountID, collectedIngredients);
            expedition.Explorer.ExperiencePoints += ingredientService.CalculateExperienceReward(collectedIngredients);
            context.Remove(expedition);

            context.SaveChanges();

            return collectedIngredients;
        }

        public double? ExpeditionSecondsRemaining(int expeditionID)
        {
            Expedition expedition = context.Expeditions.SingleOrDefault(expedition => expedition.ExpeditionID == expeditionID);
            if (expedition == null)
                throw new ExpeditionNotFoundException();

            DateTime departure = expedition.DepartureTime;
            TimeSpan timeSinceDeparture = DateTime.UtcNow - departure;
            double secondsSinceDeparture = timeSinceDeparture.TotalSeconds;

            context.Entry(expedition).Reference(expedition => expedition.EnvironmentLocation).Load();
            double timeRemaining = expedition.EnvironmentLocation.ExpeditionSeconds - secondsSinceDeparture;

            return timeRemaining;
        }

        public bool OwnsExpedition(IIdentity identity, int expeditionID)
        {
            Expedition expedition = context.Expeditions.SingleOrDefault(expedition => expedition.ExpeditionID == expeditionID);

            if (expedition == null)
                return false;

            Account account = identityService.GetAccountForIdentity(identity);
            if (account == null)
                return false;

            context.Entry(expedition).Reference(expedition => expedition.Explorer).Load();
            return expedition.Explorer.AccountID == account.AccountID;
        }
    }
}
