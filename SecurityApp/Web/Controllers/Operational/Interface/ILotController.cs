using Entity.DTO.Operational;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers.Operational.Interface
{
    public interface ILotController
    {
        Task<ActionResult<IEnumerable<LotDto>>> GetAll();
        Task<ActionResult<LotDto>> GetById(int id);
        Task<IActionResult> Add([FromForm] LotDto lotDto); // Utiliza FromForm para manejar la imagen subida
        Task<IActionResult> Update([FromForm] LotDto lotDto); // Utiliza FromForm para manejar la imagen subida
        Task<IActionResult> DeleteLogically(int id);
        Task<IActionResult> DeletePhysically(int id);
    }
}
