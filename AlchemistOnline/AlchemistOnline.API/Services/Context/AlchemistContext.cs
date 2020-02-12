using AlchemistOnline.Model;
using AlchemistOnline.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Context
{
    public class AlchemistContext: DbContext
    {
        public const string CONNECTION_STRING_NAME = "Alchemist";

        private readonly IEnumerable<IDataGenerator> generators;

        public AlchemistContext(DbContextOptions<AlchemistContext> options, IEnumerable<IDataGenerator> generators) : base(options)
        {
            this.generators = generators;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountEmail> AccountEmails { get; set; }
        public DbSet<AccountKey> AccountKeys { get; set; }

        public DbSet<EnvironmentLocation> EnvironmentLocations { get; set; }
        public DbSet<EnvironmentDifficulty> EnvironmentDifficulties { get; set; }
        public DbSet<EnvironmentType> EnvironmentTypes { get; set; }

        public DbSet<Expedition> Expeditions { get; set; }
        public DbSet<Explorer> Explorers { get; set; }
        public DbSet<ExplorerType> ExplorerTypes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientAmount> IngredientAmounts { get; set; }
        public DbSet<IngredientType> IngredientTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasIndex(account => account.DisplayName)
                .IsUnique();

            modelBuilder.Entity<AccountEmail>()
                .HasIndex(email => email.EmailAddress)
                .IsUnique();

            modelBuilder.Entity<AccountKey>()
                .HasIndex(key => key.AccountID)
                .IsUnique();

            foreach (IDataGenerator generator in generators)
                generator.Generate(modelBuilder);
        }
    }
}
