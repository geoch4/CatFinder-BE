using ApplicationLayer.Reports.Commands.CreateCommentReport;
using ApplicationLayer.Reports.Commands.CreateReport;
using ApplicationLayer.Reports.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [ApiController]
    [Route("api/advertisements/{advertisementId:int}/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly ISender _mediator;

        public ReportsController(ISender mediator) => _mediator = mediator;

        // POST /api/advertisements/{advertisementId}/reports
        // Creates a report on an advertisement. Requires authentication.
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ReportResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(int advertisementId, [FromBody] CreateReportDto dto)
        {
            var result = await _mediator.Send(new CreateReportCommand(advertisementId, dto));
            if (!result.IsSuccess) return result.Errors.Contains("Advertisement not found.")
                ? NotFound(result) : BadRequest(result);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        // POST /api/advertisements/{advertisementId}/comments/{commentId}/reports
        // Creates a report on a specific comment. Requires authentication.
        [HttpPost("/api/advertisements/{advertisementId:int}/comments/{commentId:int}/reports")]
        [Authorize]
        [ProducesResponseType(typeof(ReportResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCommentReport(
            int advertisementId, int commentId, [FromBody] CreateReportDto dto)
        {
            var result = await _mediator.Send(new CreateCommentReportCommand(advertisementId, commentId, dto));
            if (!result.IsSuccess) return result.Errors.Contains("Comment not found.")
                ? NotFound(result) : BadRequest(result);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
