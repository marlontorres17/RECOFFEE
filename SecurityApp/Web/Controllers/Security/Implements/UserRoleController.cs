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
    public class UserRoleController : ControllerBase, IUserRoleController
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetAll()
        {
            var userRoles = await _userRoleService.GetAll();
            return Ok(userRoles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleDto>> GetById(int id)
        {
            var userRole = await _userRoleService.GetById(id);
            if (userRole == null)
            {
                return NotFound();
            }
            return Ok(userRole);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserRoleDto userRoleDto)
        {
            await _userRoleService.Add(userRoleDto);
            return CreatedAtAction(nameof(GetById), new { id = userRoleDto.Id }, userRoleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserRoleDto userRoleDto)
        {
            await _userRoleService.Update(userRoleDto);
            return NoContent();
        }

        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _userRoleService.DeleteLogically(id);
            return NoContent();
        }

        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _userRoleService.DeletePhysically(id);
            return NoContent();
        }
    }
}
