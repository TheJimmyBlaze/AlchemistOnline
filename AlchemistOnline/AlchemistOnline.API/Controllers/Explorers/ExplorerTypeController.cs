using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer.Explorers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlchemistOnline.API.Controllers.Explorers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExplorerTypeController : ControllerBase
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;

        public ExplorerTypeController(AlchemistContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/ExplorerType
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ExplorerType> types = context.ExplorerTypes;
            return Ok(mapper.Map<IEnumerable<ExplorerTypeDTO>>(types));
        }

        // GET: api/ExplorerType/<ID>
        [HttpGet("{explorerTypeID}")]
        public IActionResult Get(int explorerTypeID)
        {
            ExplorerType type = context.ExplorerTypes.SingleOrDefault(type => type.ExplorerTypeID == explorerTypeID);

            if (type == null)
                return NotFound();

            return Ok(mapper.Map<ExplorerTypeDTO>(type));
        }
    }
}