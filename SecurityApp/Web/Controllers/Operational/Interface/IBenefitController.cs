using Entity.DTO.Operational;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Operational.Interface
{
    public interface IBenefitController
    {
        Task<ActionResult<IEnumerable<BenefitDto>>> GetAll();
        Task<ActionResult<BenefitDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] BenefitDto benefitDto);
        Task<IActionResult> Update([FromBody] BenefitDto benefitDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
