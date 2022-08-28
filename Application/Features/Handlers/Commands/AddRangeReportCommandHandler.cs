﻿using Application.Dtos;
using Application.Features.Request.Commands;
using Application.Interfaces.Repository;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using GemBox.Spreadsheet;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Handlers.Commands
{
    public class AddRangeReportCommandHandler : IRequestHandler<AddRangeReportCommand, IDataResult<ICollection<ReportDto>>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public AddRangeReportCommandHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }
        public async Task<IDataResult<ICollection<ReportDto>>> Handle(AddRangeReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapped = _mapper.Map<ICollection<Report>>(request.Report);
                await _reportRepository.AddRangeAsync(mapped);
                var getlist = await _reportRepository.GetListAsync();
                var result = _mapper.Map<List<ReportDto>>(getlist);
                var distinct = result.GroupBy(g => g.Location).Select(s => s.Last()).ToList();

                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                ExcelFile workbook = new ExcelFile();
                ExcelWorksheet worksheet = workbook.Worksheets.Add("Users");
                worksheet.Cells[0, 0].Value = "Location";
                worksheet.Cells[0, 1].Value = "Contact Count";
                worksheet.Cells[0, 2].Value = "Phone Count";
                worksheet.Cells[0, 3].Value = "State";

                int row = 0;
                foreach (var report in distinct)
                {
                    worksheet.Cells[++row, 0].Value = report.Location;
                    worksheet.Cells[row, 1].Value = report.ContactCount;
                    worksheet.Cells[row, 2].Value = report.PhoneCount;
                    worksheet.Cells[row, 3].Value = report.State;
                }
                workbook.Save(Directory.GetCurrentDirectory() + "-report-results-" + Guid.NewGuid().ToString() + ".xlsx");

                return new SuccessDataResult<ICollection<ReportDto>>(distinct, Messages.Success_Added);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ICollection<ReportDto>>(ex.Message);
            }
        }
    }
}