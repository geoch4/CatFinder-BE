using ApplicationLayer.CatReport.Commands.CreateCatReport;
using ApplicationLayer.CatReport.Commands.DeleteCatReport;
using ApplicationLayer.CatReport.Commands.UpdateCatReport;
using ApplicationLayer.CatReport.DTOs;
using ApplicationLayer.CatReport.Queries.GetAllCatReports;
using ApplicationLayer.CatReport.Queries.GetCatReportbyId;
using DomainLayer.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    // Handles Lost and Found cat advertisements.
    // An advertisement always belongs to an account and references a cat and a location.
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementsController : ControllerBase
    {
        private readonly ISender _mediator;

        public AdvertisementsController(ISender mediator) => _mediator = mediator;

        // GET /api/advertisements
        // GET /api/advertisements?type=Lost
        // GET /api/advertisements?city=Göteborg
        // Returns all advertisements, with optional filtering by type and/or city.
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AdvertisementResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] AdvertisementType? type,
            [FromQuery] string? city)
        {
            var result = await _mediator.Send(new GetAllAdvertisementsQuery(type, city));
            return Ok(result);
        }

        // GET /api/advertisements/{id}
        // Returns a single advertisement by its id.
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AdvertisementResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetAdvertisementByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }

        // POST /api/advertisements
        // Creates a new Lost or Found advertisement.
        // Requires an existing CatId and LocationId in the request body.
        [HttpPost]
        [ProducesResponseType(typeof(AdvertisementResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateAdvertisementDto dto)
        {
            var result = await _mediator.Send(new CreateAdvertisementCommand(dto));
            if (!result.IsSuccess) return BadRequest(result);
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.AdvertisementId }, result);
        }

        // PUT /api/advertisements/{id}
        // Updates the details of an advertisement (title, description, contact info, etc.).
        // Only the owner of the advertisement should be allowed to update it.
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(AdvertisementResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAdvertisementDto dto)
        {
            var result = await _mediator.Send(new UpdateAdvertisementCommand(id, dto));
            if (!result.IsSuccess) return result.Errors.Contains("Advertisement not found.")
                ? NotFound(result) : BadRequest(result);
            return Ok(result);
        }

        // PUT /api/advertisements/{id}/status
        // Changes only the status of an advertisement (Active → Resolved or Closed).
        // Kept as a separate endpoint because status change is a distinct user action.
        [HttpPut("{id:int}/status")]
        [ProducesResponseType(typeof(AdvertisementResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] AdvertisementStatus status)
        {
            var result = await _mediator.Send(new UpdateAdvertisementStatusCommand(id, status));
            if (!result.IsSuccess) return result.Errors.Contains("Advertisement not found.")
                ? NotFound(result) : BadRequest(result);
            return Ok(result);
        }

        // DELETE /api/advertisements/{id}
        // Removes an advertisement. Only the owner or an admin should be allowed.
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAdvertisementCommand(id));
            if (!result.IsSuccess) return NotFound(result);
            return NoContent();
        }
    }
}
