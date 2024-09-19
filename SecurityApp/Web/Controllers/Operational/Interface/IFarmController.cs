using Entity.DTO.Operational;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Operational.Interface
{
    public interface IFarmController
    {
        Task<ActionResult<IEnumerable<FarmDto>>> GetAll();
        Task<ActionResult<FarmDto>> GetById(int id);
        Task<IActionResult> Add([FromForm] FarmDto farmDto); // Utiliza FromForm para manejar la imagen subida
        Task<IActionResult> Update([FromForm] FarmDto farmDto); // Utiliza FromForm para manejar la imagen subida

        // Método para eliminación lógica
        Task<IActionResult> DeleteLogically(int id);

        // Método para eliminación física
        Task<IActionResult> DeletePhysically(int id);
    }
}
