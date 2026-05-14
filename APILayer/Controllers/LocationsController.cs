using ApplicationLayer.Location.DTOs;
using ApplicationLayer.Location.Queries.GetAllLocations;
using ApplicationLayer.Location.Queries.GetLocationbyId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    // Handles location data used when creating advertisements.
    // Locations are shared across advertisements — a location is picked from a list
    // rather than free-typed, to keep city names consistent for filtering.
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ISender _mediator;

        public LocationsController(ISender mediator) => _mediator = mediator;

        // GET /api/locations
        // Returns all available locations. Used to populate the location picker when
        // creating an advertisement.
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocationResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllLocationsQuery());
            return Ok(result);
        }

        // GET /api/locations/{id}
        // Returns a single location by its id.
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(LocationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetLocationByIdQuery(id));
            if (!result.IsSuccess) return NotFound(result);
            return Ok(result);
        }
    }
}
