using Entity.DTO.Operational;
using Microsoft.AspNetCore.Mvc;
using Service.Operational.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Controllers.Operational.Interface;

namespace Web.Controllers.Operational
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionDetailController : ControllerBase, ICollectionDetailController
    {
        private readonly ICollectionDetailService _collectionDetailService;

        public CollectionDetailController(ICollectionDetailService collectionDetailService)
        {
            _collectionDetailService = collectionDetailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionDetailDto>>> GetAll()
        {
            var collectionDetails = await _collectionDetailService.GetAll();
            return Ok(collectionDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionDetailDto>> GetById(int id)
        {
            var collectionDetail = await _collectionDetailService.GetById(id);
            if (collectionDetail == null)
            {
                return NotFound();
            }
            return Ok(collectionDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CollectionDetailDto collectionDetailDto)
        {
            await _collectionDetailService.Add(collectionDetailDto);
            return CreatedAtAction(nameof(GetById), new { id = collectionDetailDto.Id }, collectionDetailDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CollectionDetailDto collectionDetailDto)
        {
            await _collectionDetailService.Update(collectionDetailDto);
            return NoContent();
        }

        [HttpDelete("logical/{id}")]
        public async Task<IActionResult> DeleteLogically(int id)
        {
            await _collectionDetailService.DeleteLogically(id);
            return NoContent();
        }

        [HttpDelete("physical/{id}")]
        public async Task<IActionResult> DeletePhysically(int id)
        {
            await _collectionDetailService.DeletePhysically(id);
            return NoContent();
        }
    }
}
