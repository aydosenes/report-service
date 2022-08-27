using Application.Dtos;
using Application.Results;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Request.Queries
{
    public class GetReportQuery : IRequest<IDataResult<List<ReportDto>>>
    {
        public GetContactListWithContactDetailListQuery GetContactListWithContactDetailListQuery { get; set; }
    }
}
