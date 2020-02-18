using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer.Explorers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Mappings
{
    public class ExplorerProfile: Profile
    {
        public ExplorerProfile()
        {
            CreateMap<Explorer, ExplorerDTO>()
                .ForMember(dto => dto.Level, option => option.MapFrom<ExplorerLevelResolver>());

            CreateMap<ExplorerType, ExplorerTypeDTO>();
            CreateMap<Expedition, ExpeditionDTO>();
        }
    }
}
