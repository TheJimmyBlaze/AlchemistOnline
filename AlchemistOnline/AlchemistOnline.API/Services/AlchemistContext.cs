using AlchemistOnline.Model;
using AlchemistOnline.Model.Database;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services
{
    public class AlchemistContext: DbContext
    {
        public AlchemistContext() { }

        public AlchemistContext(IConfiguration config) : base(config.GetConnectionString("Alchemist")) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountEmail> AccountEmails { get; set; }
        public DbSet<AccountKey> AccountKeys { get; set; }
        public DbSet<AccountToken> AccountTokens { get; set; }

        public DbSet<Environment> Environments { get; set; }
        public DbSet<EnvironmentDifficulty> EnvironmentDifficulties { get; set; }
        public DbSet<EnvironmentType> EnvironmentTypes { get; set; }

        public DbSet<Expedition> Expeditions { get; set; }
        public DbSet<Explorer> Explorers { get; set; }
        public DbSet<ExplorerType> ExplorerTypes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientAmount> IngredientAmounts { get; set; }
        public DbSet<IngredientType> IngredientTypes { get; set; }
    }
}
