using Entity.DTO.Parameter;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Parameter.Interface
{
    public interface ICityController
    {
        Task<ActionResult<IEnumerable<CityDto>>> GetAll();
        Task<ActionResult<CityDto>> GetById(int id);
        Task<IActionResult> Add(CityDto cityDto);
        Task<IActionResult> Update(CityDto cityDto);
     

        // Método para eliminación lógica
        Task<IActionResult> DeleteLogically(int id);

        // Método para eliminación física
        Task<IActionResult> DeletePhysically(int id);
    }
}
