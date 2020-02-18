using AlchemistOnline.Model.Display;
using AlchemistOnline.Model.Transfer.Accounts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.ConsoleApp.Mappings
{
    public class AccountProfile: Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountDTO, Account>();
        }
    }
}
