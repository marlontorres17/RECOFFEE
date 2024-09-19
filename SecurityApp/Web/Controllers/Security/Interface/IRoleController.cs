using Entity.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Security.Interface
{
    public interface IRoleController
    {
        Task<ActionResult<IEnumerable<RoleDto>>> GetAll();
        Task<ActionResult<RoleDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] RoleDto roleDto);
        Task<IActionResult> Update([FromBody] RoleDto roleDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
