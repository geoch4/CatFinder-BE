using ApplicationLayer.AdvertisementImages.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementImagesController : ControllerBase
    {
        [HttpGet("advertisement/{advertisementId:int}")]
        [ProducesResponseType(typeof(IEnumerable<AdvertisementImageResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AdvertisementImageResponseDto>>> GetByAdvertisement(int advertisementId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AdvertisementImageResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdvertisementImageResponseDto>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(typeof(AdvertisementImageResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AdvertisementImageResponseDto>> Create([FromBody] CreateAdvertisementImageDto dto)
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
