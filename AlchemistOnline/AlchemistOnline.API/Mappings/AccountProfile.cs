using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Mappings
{
    public class AccountProfile: Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, DisplayAccount>();
        }
    }
}
