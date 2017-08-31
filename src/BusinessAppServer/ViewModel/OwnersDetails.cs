using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
    public class OwnersDetails
    {
        public string orderId { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string pinCode { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string state { get; set; }
        public string verification { get; set; }
        public string landmark { get; set; }
        public string medicineId { get; set; }
        public string medicineName { get; set; }
        public string medicineManufacturer { get; set; }
        public string filterId { get; set; }
        public string payment { get; set; }
        public bool isActive { get; set; }
        public DateTime orderPlacedOn { get; set; }
        public DateTime orderCompleted { get; set; }
    }
}
