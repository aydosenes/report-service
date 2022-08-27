using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class ContactListWithContactDetailListDto
    {
        public List<ContactWithContactDetailListDto> ContactWithContactDetailListDto { get; set; }
    }

    public class ContactWithContactDetailListDto
    {
        public ContactDto Contact { get; set; }
        public ICollection<ContactDetailDto> ContactDetailList { get; set; }
    }

    public class ContactDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ICollection<string> ContactDetailIdList { get; set; }
    }

    public class ContactDetailDto
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContactId { get; set; }
    }
}
