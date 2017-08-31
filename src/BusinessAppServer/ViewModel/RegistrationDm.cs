using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
    [BsonIgnoreExtraElements]
    public class RegistrationDm
    {
        
        public string userNameDm { get; set; }
        public string mobileNumberDm{ get; set; }
        public string emailDm { get; set; }
        public string newPasswordDm { get; set; }
        public string dobDm { get; set; }
        public string genderDm { get; set; }
          public string tempPassword { get; set; }
    }
}
