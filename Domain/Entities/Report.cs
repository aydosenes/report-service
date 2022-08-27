using Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Common.Enums;

namespace Domain.Entities
{
    public class Report : BaseEntity
    {
        public string Location { get; set; }
        public int ContactCount { get; set; }
        public int PhoneCount { get; set; }
        public State State { get; set; }
    }
}

