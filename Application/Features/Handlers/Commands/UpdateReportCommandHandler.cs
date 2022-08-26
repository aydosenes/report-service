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
    public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand, IDataResult<Report>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public UpdateReportCommandHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<Report>> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapped = _mapper.Map<Report>(request.Report);
                var result = await _reportRepository.UpdateAsync(mapped);
                return new SuccessDataResult<Report>(result, Messages.Success_Updated);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Report>(ex.Message);
            }
        }
    }
}