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
    public class PersonController : ControllerBase, IPersonController
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            var persons = await _personService.GetAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetById(int id)
        {
            var person = await _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PersonDto personDto)
        {
            await _personService.Add(personDto);
            return CreatedAtAction(nameof(GetById), new { id = personDto.Id }, personDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] PersonDto personDto)
        {
            await _personService.Update(personDto);
            return NoContent();
        }

        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _personService.DeleteLogically(id);
            return NoContent();
        }

        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _personService.DeletePhysically(id);
            return NoContent();
        }
    }
}
