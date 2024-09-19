using Entity.DTO.Operational;
using Microsoft.AspNetCore.Mvc;
using Service.Operational.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Controllers.Operational.Interface;

namespace Web.Controllers.Operational
{
    [ApiController]
    [Route("api/[controller]")]
    public class HarvestController : ControllerBase, IHarvestController
    {
        private readonly IHarvestService _harvestService;

        public HarvestController(IHarvestService harvestService)
        {
            _harvestService = harvestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HarvestDto>>> GetAll()
        {
            var harvests = await _harvestService.GetAll();
            return Ok(harvests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HarvestDto>> GetById(int id)
        {
            var harvest = await _harvestService.GetById(id);
            if (harvest == null)
            {
                return NotFound();
            }
            return Ok(harvest);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] HarvestDto harvestDto)
        {
            await _harvestService.Add(harvestDto);
            return CreatedAtAction(nameof(GetById), new { id = harvestDto.Id }, harvestDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] HarvestDto harvestDto)
        {
            await _harvestService.Update(harvestDto);
            return NoContent();
        }

        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _harvestService.DeleteLogically(id);
            return NoContent();
        }

        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _harvestService.DeletePhysically(id);
            return NoContent();
        }
    }
}
