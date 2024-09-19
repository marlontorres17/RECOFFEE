using Entity.DTO.Parameter;
using Microsoft.AspNetCore.Mvc;


using Web.Controllers.Parameter.Interface;
using Service.Parameter.Interface;
using Service.Parameter.Implements;

namespace Web.Controller.Implements
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase, IDepartmentController
    {
        private readonly IDepartmentService _departmentService;


        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            var departments = await _departmentService.GetAll();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            var department = await _departmentService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentDto departmentDto)
        {
            await _departmentService.Add(departmentDto);
            return CreatedAtAction(nameof(GetById), new { id = departmentDto.Id }, departmentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(DepartmentDto departmentDto)
        {
            await _departmentService.Update(departmentDto);
            return NoContent();
        }

        // Eliminación lógica
        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _departmentService.DeleteLogically(id);
            return NoContent();
        }

        // Eliminación física
        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _departmentService.DeletePhysically(id);
            return NoContent();
        }
    }
}
