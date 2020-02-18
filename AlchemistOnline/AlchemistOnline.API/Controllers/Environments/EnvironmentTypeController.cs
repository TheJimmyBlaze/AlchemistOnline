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
    public class EnvironmentTypeController : ControllerBase
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;

        public EnvironmentTypeController(AlchemistContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/EnvironmentType
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<EnvironmentType> types = context.EnvironmentTypes;
            return Ok(mapper.Map<IEnumerable<EnvironmentTypeDTO>>(types));
        }

        // GET: api/EnvironmentType/<ID>
        [HttpGet("{environmentTypeID}")]
        public IActionResult Get(int environmentTypeID)
        {
            EnvironmentType type = context.EnvironmentTypes.SingleOrDefault(type => type.EnvironmentTypeID == environmentTypeID);

            if (type == null)
                return NotFound();

            return Ok(mapper.Map<EnvironmentTypeDTO>(type));
        }
    }
}