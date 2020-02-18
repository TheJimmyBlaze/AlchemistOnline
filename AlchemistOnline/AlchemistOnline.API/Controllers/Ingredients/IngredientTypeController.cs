using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer.Ingredients;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlchemistOnline.API.Controllers.Ingredients
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientTypeController : ControllerBase
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;

        public IngredientTypeController(AlchemistContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/IngredientType
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<IngredientType> types = context.IngredientTypes;
            return Ok(mapper.Map<IEnumerable<IngredientTypeDTO>>(types));
        }

        // GET: api/IngredientType/<ID>
        [HttpGet("{ingredientTypeID}")]
        public IActionResult Get(int ingredientTypeID)
        {
            IngredientType type = context.IngredientTypes.SingleOrDefault(type => type.IngredientTypeID == ingredientTypeID);

            if (type == null)
                return NotFound();

            return Ok(mapper.Map<IngredientTypeDTO>(type));
        }
    }
}