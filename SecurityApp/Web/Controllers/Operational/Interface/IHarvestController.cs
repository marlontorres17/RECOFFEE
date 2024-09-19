using Entity.DTO.Operational;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Operational.Interface
{
    public interface IHarvestController
    {
        Task<ActionResult<IEnumerable<HarvestDto>>> GetAll();
        Task<ActionResult<HarvestDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] HarvestDto harvestDto);
        Task<IActionResult> Update([FromBody] HarvestDto harvestDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
