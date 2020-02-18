using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.API.Services.Explorers;
using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer.Explorers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlchemistOnline.API.Controllers.Explorers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExplorerController : ControllerBase
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;

        public ExplorerController(AlchemistContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Explorer/Account/<ID>
        [HttpGet("Account/{accountID}")]
        public IActionResult GetExplorersForAccount(int accountID)
        {
            IEnumerable<Explorer> explorers = context.Explorers.Where(explorer => explorer.AccountID == accountID);
            return Ok(mapper.Map<IEnumerable<ExplorerDTO>>(explorers));
        }

        // GET: api/Explorer
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Explorer> explorers = context.Explorers;
            return Ok(mapper.Map<IEnumerable<ExplorerDTO>>(explorers));
        }

        // GET: api/Explorer/<ID>
        [HttpGet("{explorerID}")]
        public IActionResult Get(int explorerID)
        {
            Explorer explorer = context.Explorers.SingleOrDefault(explorer => explorer.ExplorerID == explorerID);

            if (explorer == null)
                return NotFound();

            return Ok(mapper.Map<ExplorerDTO>(explorer));
        }
    }
}
