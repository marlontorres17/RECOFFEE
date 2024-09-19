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
    public class RoleController : ControllerBase, IRoleController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById(int id)
        {
            var role = await _roleService.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RoleDto roleDto)
        {
            await _roleService.Add(roleDto);
            return CreatedAtAction(nameof(GetById), new { id = roleDto.Id }, roleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] RoleDto roleDto)
        {
            await _roleService.Update(roleDto);
            return NoContent();
        }

        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _roleService.DeleteLogically(id);
            return NoContent();
        }

        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _roleService.DeletePhysically(id);
            return NoContent();
        }
    }
}
