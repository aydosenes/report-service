using System;
using Application.Features.Request.Commands;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repository;

namespace Application.Features.Handlers.Commands
{
    public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, IDataResult<Report>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public DeleteReportCommandHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<Report>> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapped = _mapper.Map<Report>(request.Report);
                var result = await _reportRepository.DeleteAsync(mapped);
                return new SuccessDataResult<Report>(result, Messages.Success_Deleted);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Report>(ex.Message);
            }
        }
    }
}