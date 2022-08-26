using Application.Features.Request.Commands;
using Application.Interfaces.Repository;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Handlers.Commands
{
    public class AddReportCommandHandler : IRequestHandler<AddReportCommand, IDataResult<Report>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public AddReportCommandHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<Report>> Handle(AddReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapped = _mapper.Map<Report>(request.Report);
                var result = await _reportRepository.AddAsync(mapped);
                return new SuccessDataResult<Report>(result, Messages.Success_Added);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Report>(ex.Message);
            }
        }
    }
}