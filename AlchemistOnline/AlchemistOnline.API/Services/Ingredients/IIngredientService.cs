using AlchemistOnline.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Ingredients
{
    public interface IIngredientService
    {
        public IEnumerable<Ingredient> GenerateExpeditionReward(Expedition expedition);

        public void CommitIngredientsToAccount(int accountID, IEnumerable<Ingredient> ingredients);

        public double CalculateExperienceReward(IEnumerable<Ingredient> ingredients);
    }
}
