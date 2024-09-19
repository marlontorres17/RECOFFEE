using Entity.DTO.Security;
using Microsoft.AspNetCore.Mvc;
using Service.Security.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Controllers.Security.Interface;

namespace Web.Controllers.Security
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewController : ControllerBase, IViewController
    {
        private readonly IViewService _viewService;

        public ViewController(IViewService viewService)
        {
            _viewService = viewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewDto>>> GetAll()
        {
            var views = await _viewService.GetAll();
            return Ok(views);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewDto>> GetById(int id)
        {
            var view = await _viewService.GetById(id);
            if (view == null)
            {
                return NotFound();
            }
            return Ok(view);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ViewDto viewDto)
        {
            await _viewService.Add(viewDto);
            return CreatedAtAction(nameof(GetById), new { id = viewDto.Id }, viewDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ViewDto viewDto)
        {
            await _viewService.Update(viewDto);
            return NoContent();
        }

        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _viewService.DeleteLogically(id);
            return NoContent();
        }

        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _viewService.DeletePhysically(id);
            return NoContent();
        }
    }
}
