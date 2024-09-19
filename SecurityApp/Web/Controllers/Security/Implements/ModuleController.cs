using Entity.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using Service.Security.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Controllers.Security.Interface;

namespace Web.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase, IModuleController
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetAll()
        {
            var modules = await _moduleService.GetAll();
            return Ok(modules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetById(int id)
        {
            var module = await _moduleService.GetById(id);
            if (module == null)
            {
                return NotFound();
            }
            return Ok(module);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ModuleDto moduleDto)
        {
            await _moduleService.Add(moduleDto);
            return CreatedAtAction(nameof(GetById), new { id = moduleDto.Id }, moduleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ModuleDto moduleDto)
        {
            await _moduleService.Update(moduleDto);
            return NoContent();
        }

        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _moduleService.DeleteLogically(id);
            return NoContent();
        }

        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _moduleService.DeletePhysically(id);
            return NoContent();
        }
    }
}
