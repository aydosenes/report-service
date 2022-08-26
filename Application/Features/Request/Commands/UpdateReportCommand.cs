﻿using Application.Dtos;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Request.Commands
{
    public class UpdateReportCommand : IRequest<IDataResult<Report>>
    {
        public ReportDto Report { get; set; }
    }
}
