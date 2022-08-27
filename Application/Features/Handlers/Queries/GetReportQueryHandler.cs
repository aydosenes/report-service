using Application.Dtos;
using Application.Features.Request.Queries;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Application.Results;
using AutoMapper;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Handlers.Queries
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, IDataResult<List<ReportDto>>>
    {
        private readonly IRestService _restService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;

        public GetReportQueryHandler(IRestService restService, IMapper mapper, ISendEndpointProvider sendEndpointProvider, IReportRepository reportRepository)
        {
            _restService = restService;
            _mapper = mapper;
            _sendEndpointProvider = sendEndpointProvider;
            _reportRepository = reportRepository;
        }

        public async Task<IDataResult<List<ReportDto>>> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var send = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:contact-service"));
                await send.Send(request.GetContactListWithContactDetailListQuery);
                return new SuccessDataResult<List<ReportDto>>();

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ReportDto>>(ex.Message);
            }
        }
    }
}