using ApplicationLayer.SavedAdvertisements.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SavedAdvertisementsController : ControllerBase
    {
        [HttpGet("account/{accountId:int}")]
        [ProducesResponseType(typeof(IEnumerable<SavedAdvertisementResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SavedAdvertisementResponseDto>>> GetByAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SavedAdvertisementResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SavedAdvertisementResponseDto>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(typeof(SavedAdvertisementResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SavedAdvertisementResponseDto>> Create([FromBody] CreateSavedAdvertisementDto dto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
