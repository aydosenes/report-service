using Application.Dtos;
using Application.Features.Request.Commands;
using Application.Features.Request.Queries;
using Application.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IDataResult<ReportDto>))]
        public async Task<IActionResult> GetReportById(string id)
        {
            var result = await _mediator.Send(new GetReportByIdQuery() { Id = id });
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);

        }

        [HttpGet("report-list")]
        [ProducesResponseType(200, Type = typeof(IDataResult<List<ReportDto>>))]
        public async Task<IActionResult> GetReportList()
        {
            var result = await _mediator.Send(new GetReportListQuery());
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);

        }

        [HttpPost("add-report")]
        [ProducesResponseType(200, Type = typeof(BaseResult))]
        public async Task<IActionResult> AddReport([FromBody] AddReportCommand request)
        {
            var result = await _mediator.Send(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }

        [HttpPut("update-report")]
        [ProducesResponseType(200, Type = typeof(BaseResult))]
        public async Task<IActionResult> UpdateReport([FromBody] UpdateReportCommand request)
        {
            var result = await _mediator.Send(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }


        [HttpDelete("delete-report")]
        [ProducesResponseType(200, Type = typeof(BaseResult))]
        public async Task<IActionResult> DeleteReport([FromBody] DeleteReportCommand request)
        {
            var result = await _mediator.Send(request);
            if (result.Success)
                return Ok(result);
            return BadRequest(result.Message);
        }
    }
}
