using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
    [BsonIgnoreExtraElements]
    public class Report
    {          
        public string medicineName { get; set; }
        public string medicineManufacturer { get; set; }       
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string pinCode { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string landmark { get; set; }
        public string verification { get; set; }
        public string paymentOption { get; set; }
        public bool? isActive { get; set; }
        public DateTime? orderPlacedOn { get; set; }
        public DateTime? orderCompleted { get; set; }
    }
}
