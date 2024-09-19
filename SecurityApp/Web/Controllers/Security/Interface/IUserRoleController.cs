using Entity.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Security.Interface
{
    public interface IUserRoleController
    {
        Task<ActionResult<IEnumerable<UserRoleDto>>> GetAll();
        Task<ActionResult<UserRoleDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] UserRoleDto userRoleDto);
        Task<IActionResult> Update([FromBody] UserRoleDto userRoleDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
