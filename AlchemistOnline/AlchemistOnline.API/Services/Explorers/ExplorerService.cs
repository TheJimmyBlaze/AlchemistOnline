using AlchemistOnline.API.Services.Context;
using AlchemistOnline.Model.Database;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Explorers
{
    public class ExplorerService : IExplorerService
    {
        private readonly AlchemistContext context;
        private readonly Random random;
        private readonly PersonNameGenerator nameGenerator;
        
        public ExplorerService(AlchemistContext context, Random random, PersonNameGenerator nameGenerator)
        {
            this.context = context;
            this.random = random;
            this.nameGenerator = nameGenerator;
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
    }
}
