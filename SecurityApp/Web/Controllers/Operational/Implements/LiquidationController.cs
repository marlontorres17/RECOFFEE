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
    public class LiquidationController : ControllerBase, ILiquidationController
    {
        private readonly ILiquidationService _liquidationService;

        public LiquidationController(ILiquidationService liquidationService)
        {
            _liquidationService = liquidationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LiquidationDto>>> GetAll()
        {
            var liquidations = await _liquidationService.GetAll();
            return Ok(liquidations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LiquidationDto>> GetById(int id)
        {
            var liquidation = await _liquidationService.GetById(id);
            if (liquidation == null)
            {
                return NotFound();
            }
            return Ok(liquidation);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LiquidationDto liquidationDto)
        {
            await _liquidationService.Add(liquidationDto);
            return CreatedAtAction(nameof(GetById), new { id = liquidationDto.Id }, liquidationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] LiquidationDto liquidationDto)
        {
            await _liquidationService.Update(liquidationDto);
            return NoContent();
        }

        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _liquidationService.DeleteLogically(id);
            return NoContent();
        }

        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _liquidationService.DeletePhysically(id);
            return NoContent();
        }
    }
}
