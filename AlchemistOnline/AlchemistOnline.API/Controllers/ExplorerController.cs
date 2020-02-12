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

namespace AlchemistOnline.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExplorerController : ControllerBase
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;
        private readonly IExplorerService service;

        public ExplorerController(AlchemistContext context, IMapper mapper, IExplorerService service)
        {
            this.context = context;
            this.mapper = mapper;
            this.service = service;
        }

        // GET: api/Explorer/Account/<ID>
        [HttpGet("Account/{accountID}")]
        public IEnumerable<ExplorerDTO> GetExplorersForAccount(int accountID)
        {
            IEnumerable<Explorer> explorers = context.Explorers.Where(explorer => explorer.AccountID == accountID);
            return mapper.Map<IEnumerable<ExplorerDTO>>(explorers);
        }

        // GET: api/Explorer
        [HttpGet]
        public IEnumerable<ExplorerDTO> Get()
        {
            IEnumerable<Explorer> explorers = context.Explorers;
            return mapper.Map<IEnumerable<ExplorerDTO>>(explorers);
        }

        // GET: api/Explorer/5
        [HttpGet("{explorerID}")]
        public ExplorerDTO Get(int explorerID)
        {
            Explorer explorer = context.Explorers.SingleOrDefault(explorer => explorer.ExplorerID == explorerID);
            return mapper.Map<ExplorerDTO>(explorer);
        }

        // POST: api/Explorer
        [HttpPost]
        public void Post([FromBody] ExplorerDTO explorer)
        {
        }

        // PUT: api/Explorer/5
        [HttpPut("{explorerID}")]
        public void Put(int explorerID, [FromBody] ExplorerDTO explorer)
        {
        }
    }
}
