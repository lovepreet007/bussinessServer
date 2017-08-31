using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
   
    public class OrderDetailVm
    {   public string orderId { get; set; }
        public  Address address { get; set; }
        public  ProductToBuy productToBuy { get; set; }
        public string payment { get; set; }
        public bool isActive{get;set;}        
        public DateTime orderPlacedOn { get; set; }
        public DateTime orderCompleted { get; set; }
    }
}
