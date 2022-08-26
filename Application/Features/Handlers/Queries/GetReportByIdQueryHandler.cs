using Application.Dtos;
using Application.Features.Request.Queries;
using Application.Interfaces.Repository;
using Application.Results;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Handlers.Queries
{
    public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, IDataResult<ReportDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public GetReportByIdQueryHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<ReportDto>> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var get = await _reportRepository.GetByIdAsync(request.Id);
                var result = _mapper.Map<ReportDto>(get);
                return new SuccessDataResult<ReportDto>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ReportDto>(ex.Message);
            }
        }
    }
}