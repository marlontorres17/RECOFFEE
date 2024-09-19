using Entity.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Security.Interface
{
    public interface IRoleViewController
    {
        Task<ActionResult<IEnumerable<RoleViewDto>>> GetAll();
        Task<ActionResult<RoleViewDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] RoleViewDto roleViewDto);
        Task<IActionResult> Update([FromBody] RoleViewDto roleViewDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
