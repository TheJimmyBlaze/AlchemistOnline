using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer.Ingredients;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlchemistOnline.API.Controllers.Ingredients
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase  
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;

        public IngredientController(AlchemistContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Ingredient/Account/<AccountID>
        [HttpGet("Account/{accountID}")]
        public IActionResult GetIngredientAmountsForAccount(int accountID)
        {
            IEnumerable<IngredientAmount> amounts = context.IngredientAmounts.Where(amount => amount.AccountID == accountID);
            return Ok(mapper.Map<IEnumerable<IngredientAmountDTO>>(amounts));
        }

        // GET: api/Ingredient
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Ingredient> ingredients = context.Ingredients;
            return Ok(mapper.Map<IEnumerable<IngredientDTO>>(ingredients));
        }

        //GET: api/Ingredient/<ID>
        [HttpGet("{ingredientID}")]
        public IActionResult Get(int ingredientID)
        {
            Ingredient ingredient = context.Ingredients.SingleOrDefault(ingredient => ingredient.IngredientID == ingredientID);

            if (ingredient == null)
                return NotFound();

            return Ok(mapper.Map<IngredientDTO>(ingredient));
        }
    }
}