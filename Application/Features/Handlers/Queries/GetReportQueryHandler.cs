using Application.Dtos;
using Application.Features.Request.Queries;
using Application.Interfaces.Repository;
using Application.Interfaces.Service;
using Application.Results;
using AutoMapper;
using MassTransit;
using MediatR;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Handlers.Queries
{
    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, IDataResult<MemoryStream>>
    {
        private readonly IRestService _restService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;
        private readonly IPublishEndpoint _endpoint;        
        private readonly IRequestClient<AddRangeReportDto> _client;
        private readonly IBus _bus;
        private readonly IBusControl _busControl;

        public GetReportQueryHandler(IRestService restService, IMapper mapper, ISendEndpointProvider sendEndpointProvider, IReportRepository reportRepository, IRequestClient<AddRangeReportDto> client, IBus bus, IPublishEndpoint endpoint, IBusControl busControl)
        {
            _restService = restService;
            _mapper = mapper;
            _sendEndpointProvider = sendEndpointProvider;
            _reportRepository = reportRepository;
            _client = client;
            _bus = bus;
            _endpoint = endpoint;
            _busControl = busControl;
        }

        public async Task<IDataResult<MemoryStream>> Handle(GetReportQuery query, CancellationToken cancellationToken)
        {
            try
            {
                Task.Run(async () => {
                    var send = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:contact-service"));
                    query.GetContactListWithContactDetailListQuery.UUID = Guid.NewGuid().ToString();
                    await send.Send(query.GetContactListWithContactDetailListQuery);
                }).Wait();

                var result = await _reportRepository.GetListAsync();
                var reports = _mapper.Map<List<ReportDto>>(result);
                var distinct = reports.GroupBy(g => g.Location).Select(s => s.Last()).ToList();

                var memory = new MemoryStream();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(memory))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                    workSheet.Cells.LoadFromCollection(distinct, true);
                    package.Save();
                }
                memory.Position = 0;

                return new SuccessDataResult<MemoryStream>(memory);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<MemoryStream>(ex.Message);
            }
        }
    }
}