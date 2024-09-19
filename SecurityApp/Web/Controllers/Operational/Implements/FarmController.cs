using Entity.DTO.Operational;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Operational.Interface;
using Service.Operational.Interface;

namespace Web.Controllers.Operational.Implements
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmController : ControllerBase, IFarmController
    {
        private readonly IFarmService _farmService;

        public FarmController(IFarmService farmService)
        {
            _farmService = farmService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FarmDto>>> GetAll()
        {
            var farms = await _farmService.GetAll();
            return Ok(farms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FarmDto>> GetById(int id)
        {
            var farm = await _farmService.GetById(id);
            if (farm == null)
            {
                return NotFound();
            }
            return Ok(farm);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] FarmDto farmDto) // Utiliza FromForm para manejar la imagen subida
        {
            await _farmService.Add(farmDto);
            return CreatedAtAction(nameof(GetById), new { id = farmDto.Id }, farmDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] FarmDto farmDto) // Utiliza FromForm para manejar la imagen subida
        {
            await _farmService.Update(farmDto);
            return NoContent();
        }

        // Eliminación lógica
        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _farmService.DeleteLogically(id);
            return NoContent();
        }

        // Eliminación física
        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _farmService.DeletePhysically(id);
            return NoContent();
        }
    }
}
