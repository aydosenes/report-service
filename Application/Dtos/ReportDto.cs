using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Common.Enums;

namespace Application.Dtos
{
    public class ReportDto
    {
        public string Location { get; set; }
        public int ContactCount { get; set; }
        public int PhoneCount { get; set; }
        public State State { get; set; }
    }
}
