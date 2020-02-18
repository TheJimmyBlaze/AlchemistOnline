using AlchemistOnline.API.Services.Context;
using AlchemistOnline.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Ingredients
{
    public class IngredientService : IIngredientService
    {
        private readonly AlchemistContext context;
        private readonly Random random;

        public IngredientService(AlchemistContext context, Random random)
        {
            this.context = context;
            this.random = random;
        }

        public IEnumerable<Ingredient> GenerateExpeditionReward(Expedition expedition)
        {
            context.Entry(expedition).Reference(expedition => expedition.EnvironmentLocation).Load();
            context.Entry(expedition.EnvironmentLocation).Reference(Environment => Environment.EnvironmentDifficulty).Load();

            IEnumerable<Ingredient> environmentIngredients = context.Ingredients.Where(ingredient => ingredient.EnvironmentTypeID == null ||
                                                                                        ingredient.EnvironmentTypeID == expedition.EnvironmentLocation.EnvironmentTypeID).
                                                                                        AsEnumerable();

            int minIngredients = 1;
            int maxIngredients = expedition.EnvironmentLocation.EnvironmentDifficulty.RewardMultiplier;
            int collectedIngredients = random.Next(minIngredients, maxIngredients);

            List<Ingredient> ingredients = new List<Ingredient>();
            for(int i = 0; i < collectedIngredients; i++)
            {
                int index = random.Next(0, environmentIngredients.Count());
                ingredients.Add(environmentIngredients.ElementAt(index));
            }

            return ingredients;
        }

        public void CommitIngredientsToAccount(int accountID, IEnumerable<Ingredient> ingredients)
        {
            ICollection<IngredientAmount> ingredientAmounts = context.IngredientAmounts.Where(amount => amount.AccountID == accountID).ToList();

            foreach(Ingredient ingredient in ingredients)
            {
                IngredientAmount amount = ingredientAmounts.SingleOrDefault(amount => amount.IngredientID == ingredient.IngredientID);

                if (amount == null)
                {
                    amount = new IngredientAmount
                    {
                        AccountID = accountID,
                        IngredientID = ingredient.IngredientID,
                        Amount = 0
                    };
                    ingredientAmounts.Add(amount);
                }

                amount.Amount++;
            }

            Account account = context.Accounts.Single(account => account.AccountID == accountID);
            account.IngredientAmounts = ingredientAmounts.ToList();

            context.SaveChanges();
        }
        
        public double CalculateExperienceReward(IEnumerable<Ingredient> ingredients)
        {
            return ingredients.Count() * 3;
        }
    }
}
