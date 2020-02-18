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
    public class EnvironmentLocationController : ControllerBase
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;

        public EnvironmentLocationController(AlchemistContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/EnvironmentLocation
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<EnvironmentLocation> types = context.EnvironmentLocations;
            return Ok(mapper.Map<IEnumerable<EnvironmentLocationDTO>>(types));
        }

        // GET: api/EnvironmentLocation/<ID>
        [HttpGet("{environmentLocationID}")]
        public IActionResult Get(int environmentLocationID)
        {
            EnvironmentLocation location = context.EnvironmentLocations.SingleOrDefault(location => location.EnvironmentLocationID == environmentLocationID);

            if (location == null)
                return NotFound();

            return Ok(mapper.Map<EnvironmentLocationDTO>(location));
        }
    }
}