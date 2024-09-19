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
    public class RoleViewController : ControllerBase, IRoleViewController
    {
        private readonly IRoleViewService _roleViewService;

        public RoleViewController(IRoleViewService roleViewService)
        {
            _roleViewService = roleViewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleViewDto>>> GetAll()
        {
            var roleViews = await _roleViewService.GetAll();
            return Ok(roleViews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleViewDto>> GetById(int id)
        {
            var roleView = await _roleViewService.GetById(id);
            if (roleView == null)
            {
                return NotFound();
            }
            return Ok(roleView);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RoleViewDto roleViewDto)
        {
            await _roleViewService.Add(roleViewDto);
            return CreatedAtAction(nameof(GetById), new { id = roleViewDto.Id }, roleViewDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] RoleViewDto roleViewDto)
        {
            await _roleViewService.Update(roleViewDto);
            return NoContent();
        }

        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _roleViewService.DeleteLogically(id);
            return NoContent();
        }

        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _roleViewService.DeletePhysically(id);
            return NoContent();
        }
    }
}
