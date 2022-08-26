using Application.Dtos;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Request.Commands
{
    public class DeleteReportCommand : IRequest<IDataResult<Report>>
    {
        public ReportDto Report { get; set; }
    }
}
