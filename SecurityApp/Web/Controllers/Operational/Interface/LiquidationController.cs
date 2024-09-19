using Entity.DTO.Operational;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Operational.Interface
{
    public interface ILiquidationController
    {
        Task<ActionResult<IEnumerable<LiquidationDto>>> GetAll();
        Task<ActionResult<LiquidationDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] LiquidationDto liquidationDto);
        Task<IActionResult> Update([FromBody] LiquidationDto liquidationDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
