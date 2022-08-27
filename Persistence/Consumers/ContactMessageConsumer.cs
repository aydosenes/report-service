using Application.Dtos;
using MassTransit;
using System;
using MediatR;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Domain.Common;
using Application.Features.Request.Commands;

namespace Persistence.Consumers
{
    public class ContactMessageConsumer : IConsumer<ContactListWithContactDetailListDto>
    {
        private readonly IMediator _mediator;
        public ContactMessageConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<ContactListWithContactDetailListDto> context)
        {
            var reportList = new List<ReportDto>();
            var dto = context.Message.ContactWithContactDetailListDto;
            var groupedDetailList = dto.SelectMany(s => s.ContactDetailList).GroupBy(g => g.Location.Trim().ToLower()).ToList();
            foreach (var groupedDetail in groupedDetailList)
            {
                reportList.Add(new ReportDto()
                {
                    Location = groupedDetail.Key,
                    PhoneCount = groupedDetail.Count(),
                    ContactCount = groupedDetail.Select(s => s.ContactId).Distinct().ToList().Count,
                    State = Enums.State.InProgress
                });
            }
            var result = await _mediator.Send(new AddRangeReportCommand() { Report = reportList });
        }
    }
}
