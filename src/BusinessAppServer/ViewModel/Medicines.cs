using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
    [BsonIgnoreExtraElements]
    public class Medicines
    {
        public string medicineId { get; set; }
        public string medicineName { get; set; }
        public string medicineManufacturer { get; set; }
        public string filterId { get; set; }       
    }
}
