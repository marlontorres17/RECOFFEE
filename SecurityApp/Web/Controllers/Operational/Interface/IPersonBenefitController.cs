using Entity.DTO.Operational;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Operational.Interface
{
    public interface IPersonBenefitController
    {
        Task<ActionResult<IEnumerable<PersonBenefitDto>>> GetAll();
        Task<ActionResult<PersonBenefitDto>> GetById(int id);
        Task<IActionResult> Add([FromBody] PersonBenefitDto personBenefitDto);
        Task<IActionResult> Update([FromBody] PersonBenefitDto personBenefitDto);
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
