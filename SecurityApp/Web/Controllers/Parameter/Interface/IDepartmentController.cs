using Entity.DTO.Parameter;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Parameter.Interface
{
    public interface IDepartmentController
    {
        Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll();
        Task<ActionResult<DepartmentDto>> GetById(int id);
        Task<IActionResult> Add(DepartmentDto departmentDto);
        Task<IActionResult> Update(DepartmentDto departmentDto);
      

        // Método para eliminación lógica
        Task<IActionResult> DeleteLogically(int id);

        // Método para eliminación física
        Task<IActionResult> DeletePhysically(int id);
    }
}
