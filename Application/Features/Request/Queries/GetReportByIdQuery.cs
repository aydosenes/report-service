using Application.Dtos;
using Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Request.Queries
{
    public class GetReportByIdQuery : IRequest<IDataResult<ReportDto>>
    {
        public string Id { get; set; }
    }
}
