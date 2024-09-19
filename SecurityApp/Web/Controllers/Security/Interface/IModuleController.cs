using Entity.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Security.Interface
{
    public interface IModuleController
    {
        Task<ActionResult<IEnumerable<ModuleDto>>> GetAll();
        Task<ActionResult<ModuleDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] ModuleDto moduleDto);
        Task<IActionResult> Update([FromBody] ModuleDto moduleDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
