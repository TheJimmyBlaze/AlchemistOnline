using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer.Environments;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlchemistOnline.API.Controllers.Environments
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentDifficultyController : ControllerBase
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;

        public EnvironmentDifficultyController(AlchemistContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/EnvironmentDifficulty
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<EnvironmentDifficulty> difficulties = context.EnvironmentDifficulties;
            return Ok(mapper.Map<IEnumerable<EnvironmentDifficultyDTO>>(difficulties));
        }

        // GET: api/EnvironmentDifficulty/<ID>
        [HttpGet("{environmentDifficultyID}")]
        public IActionResult Get(int environmentDifficultyID)
        {
            EnvironmentDifficulty difficulty = context.EnvironmentDifficulties.SingleOrDefault(difficulty => difficulty.EnvironmentDifficultyID == environmentDifficultyID);

            if (difficulty == null)
                return NotFound();

            return Ok(mapper.Map<EnvironmentDifficultyDTO>(difficulty));
        }
    }
}