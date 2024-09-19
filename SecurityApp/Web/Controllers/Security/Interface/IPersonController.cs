using Entity.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Security.Interface
{
    public interface IPersonController
    {
        Task<ActionResult<IEnumerable<PersonDto>>> GetAll();
        Task<ActionResult<PersonDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] PersonDto personDto);
        Task<IActionResult> Update([FromBody] PersonDto personDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
