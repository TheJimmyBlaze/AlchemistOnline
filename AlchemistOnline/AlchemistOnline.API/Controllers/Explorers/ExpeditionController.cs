using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlchemistOnline.API.Exceptions;
using AlchemistOnline.API.Services.Context;
using AlchemistOnline.API.Services.Explorers;
using AlchemistOnline.Model.Database;
using AlchemistOnline.Model.Transfer.Explorers;
using AlchemistOnline.Model.Transfer.Ingredients;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlchemistOnline.API.Controllers.Explorers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpeditionController : ControllerBase
    {
        private readonly AlchemistContext context;
        private readonly IMapper mapper;

        private readonly IExpeditionService expeditionService;
        private readonly IExplorerService explorerService;

        public ExpeditionController(AlchemistContext context, IMapper mapper, IExpeditionService expeditionService, IExplorerService explorerService)
        {
            this.context = context;
            this.mapper = mapper;
            this.expeditionService = expeditionService;
            this.explorerService = explorerService;
        }

        // GET: api/Expedition/SecondsRemaining/<ID>
        [HttpGet("SecondsRemaining/{expeditionID}")]
        public IActionResult SecondsRemaining(int expeditionID)
        {
            try
            {
                return Ok(expeditionService.ExpeditionSecondsRemaining(expeditionID));
            }
            catch(ExpeditionNotFoundException)
            {
                return NotFound();
            } 
        }

        // GET: api/Expedition/Complete/<ID>
        [HttpGet("Complete/{expeditionID}")]
        public IActionResult CompleteExpedition(int expeditionID)
        {
            if (!expeditionService.OwnsExpedition(HttpContext.User.Identity, expeditionID))
                return Unauthorized("Account does not own this Expedition");

            try
            {
                IEnumerable<Ingredient> collectedIngredients = expeditionService.CompleteExpedition(expeditionID);
                return Ok(mapper.Map<IEnumerable<IngredientDTO>>(collectedIngredients));
            }
            catch (ExpeditionInProgressException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ExpeditionNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Expedition
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Expedition> expeditions = context.Expeditions;
            return Ok(mapper.Map<IEnumerable<ExpeditionDTO>>(expeditions));
        }

        // GET: api/Expedition/5
        [HttpGet("{expeditionID}")]
        public IActionResult Get(int expeditionID)
        {
            Expedition expedition = context.Expeditions.SingleOrDefault(expedition => expedition.ExpeditionID == expeditionID);

            if (expedition == null)
                return NotFound();

            return Ok(mapper.Map<ExpeditionDTO>(expedition));
        }

        // POST: api/Expedition
        [HttpPost]
        public IActionResult Post([FromBody] NewExpeditionDTO request)
        {
            if (!explorerService.OwnsExplorer(HttpContext.User.Identity, request.ExplorerID))
                return Unauthorized("Account does not own this Explorer");

            try
            {
                Expedition expedition = expeditionService.BeginExpedition(request.ExplorerID, request.EnvironmentLocationID);
                return Ok(mapper.Map<ExpeditionDTO>(expedition));
            }
            catch (ExplorerLevelException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
