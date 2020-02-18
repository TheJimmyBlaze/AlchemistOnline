using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer.Ingredients;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Mappings
{
    public class IngredientProfile: Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<IngredientAmount, IngredientAmountDTO>();
            CreateMap<IngredientType, IngredientTypeDTO>();
        }
    }
}
