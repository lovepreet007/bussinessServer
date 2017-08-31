using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
    [BsonIgnoreExtraElements]
    public class Filters
    {
        public string filterName { get; set; }
        public string filterId { get; set; }
    }
}
