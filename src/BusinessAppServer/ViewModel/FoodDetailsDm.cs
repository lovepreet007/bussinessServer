﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{    
    public class FoodsCollection
    {
        public string foodId { get; set; }
        public string foodName { get; set; }
    }
   
    public class FoodItemDetailsDm
    {
        public string foodId { get; set; }
        public string foodDetailsId { get; set; }
        public string foodItemName { get; set; }

        public string foodItemPrice { get; set; }
        public string foodDetailsDescription { get; set; }
    }
}
