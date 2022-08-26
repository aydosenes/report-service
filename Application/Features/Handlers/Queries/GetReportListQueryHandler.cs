using Application.Dtos;
using Application.Features.Request.Queries;
using Application.Interfaces.Repository;
using Application.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Handlers.Queries
{
    public class GetReportListQueryHandler : IRequestHandler<GetReportListQuery, IDataResult<List<ReportDto>>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public GetReportListQueryHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<ReportDto>>> Handle(GetReportListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var get = await _reportRepository.GetListAsync();
                var result = _mapper.Map<List<ReportDto>>(get);
                return new SuccessDataResult<List<ReportDto>>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ReportDto>>(ex.Message);
            }
        }
    }
}
