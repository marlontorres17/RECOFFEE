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
    public class BenefitController : ControllerBase, IBenefitController
    {
        private readonly IBenefitService _benefitService;

        public BenefitController(IBenefitService benefitService)
        {
            _benefitService = benefitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BenefitDto>>> GetAll()
        {
            var benefits = await _benefitService.GetAll();
            return Ok(benefits);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BenefitDto>> GetById(int id)
        {
            var benefit = await _benefitService.GetById(id);
            if (benefit == null)
            {
                return NotFound();
            }
            return Ok(benefit);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BenefitDto benefitDto)
        {
            await _benefitService.Add(benefitDto);
            return CreatedAtAction(nameof(GetById), new { id = benefitDto.Id }, benefitDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] BenefitDto benefitDto)
        {
            await _benefitService.Update(benefitDto);
            return NoContent();
        }

        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _benefitService.DeleteLogically(id);
            return NoContent();
        }

        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _benefitService.DeletePhysically(id);
            return NoContent();
        }
    }
}
