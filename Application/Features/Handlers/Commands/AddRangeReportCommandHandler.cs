using Application.Features.Request.Commands;
using Application.Interfaces.Repository;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Handlers.Commands
{
    public class AddRangeReportCommandHandler : IRequestHandler<AddRangeReportCommand, IDataResult<ICollection<Report>>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public AddRangeReportCommandHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<ICollection<Report>>> Handle(AddRangeReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapped = _mapper.Map<ICollection<Report>>(request.Report);
                var result = await _reportRepository.AddRangeAsync(mapped);
                return new SuccessDataResult<ICollection<Report>>(result, Messages.Success_Added);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ICollection<Report>>(ex.Message);
            }
        }
    }
}