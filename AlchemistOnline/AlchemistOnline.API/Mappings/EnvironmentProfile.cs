using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer.Environments;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Mappings
{
    public class EnvironmentProfile : Profile
    {
        public EnvironmentProfile()
        {
            CreateMap<EnvironmentDifficulty, EnvironmentDifficultyDTO>();
            CreateMap<EnvironmentType, EnvironmentTypeDTO>();
            CreateMap<EnvironmentLocation, EnvironmentLocationDTO>();
        }
    }
}
