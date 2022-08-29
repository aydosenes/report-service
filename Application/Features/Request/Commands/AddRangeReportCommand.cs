using Application.Dtos;
using Application.Results;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.IO;

namespace Application.Features.Request.Commands
{
    public class AddRangeReportCommand : IRequest<IDataResult<List<ReportDto>>>
    {
        public ICollection<ReportDto> Report { get; set; }
    }
}
