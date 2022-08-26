using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class ReportDto
    {
        public string Location { get; set; }
        public int ContactCount { get; set; }
        public int PhoneCount { get; set; }
        public byte State { get; set; }
    }
}
