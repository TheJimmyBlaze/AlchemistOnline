using AlchemistOnline.API.Services.Explorers;
using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer.Explorers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Mappings
{
    public class ExplorerLevelResolver : IValueResolver<Explorer, ExplorerDTO, double>
    {
        private readonly IExplorerService explorerService;

        public ExplorerLevelResolver(IExplorerService explorerService)
        {
            this.explorerService = explorerService;
        }

        public double Resolve(Explorer source, ExplorerDTO destination, double destMember, ResolutionContext context)
        {
            return explorerService.CalculateLevel(source.ExperiencePoints);
        }
    }
}
