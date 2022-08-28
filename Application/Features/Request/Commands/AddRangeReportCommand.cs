using Application.Dtos;
using Application.Results;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Request.Commands
{
    public class AddRangeReportCommand : IRequest<IDataResult<ICollection<ReportDto>>>
    {
        public ICollection<ReportDto> Report { get; set; }
    }
}
