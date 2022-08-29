using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class AddRangeReportDto
    {
        public string UUID { get; set; }
        public ICollection<ReportDto> Report { get; set; }
    }
}
