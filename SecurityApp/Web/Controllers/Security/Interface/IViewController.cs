using Entity.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Security.Interface
{
    public interface IViewController
    {
        Task<ActionResult<IEnumerable<ViewDto>>> GetAll();
        Task<ActionResult<ViewDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] ViewDto viewDto);
        Task<IActionResult> Update([FromBody] ViewDto viewDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
