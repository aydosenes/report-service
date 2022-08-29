using Application.Dtos;
using Application.Results;
using MediatR;
using System.Collections.Generic;
using System.IO;

namespace Application.Features.Request.Queries
{
    public class GetReportQuery : IRequest<IDataResult<MemoryStream>>
    {
        public GetContactListWithContactDetailListQuery GetContactListWithContactDetailListQuery { get; set; }
    }
}
