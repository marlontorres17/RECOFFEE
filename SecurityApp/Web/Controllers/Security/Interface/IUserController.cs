using Entity.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Security.Interface
{
    public interface IUserController
    {
        Task<ActionResult<IEnumerable<UserDto>>> GetAll();
        Task<ActionResult<UserDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] UserDto userDto);
        Task<IActionResult> Update([FromBody] UserDto userDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
