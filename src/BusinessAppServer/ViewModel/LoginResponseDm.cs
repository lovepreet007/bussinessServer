using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
    public class LoginResponseDm
    {
        public string status { get; set; }
        public string message { get; set; }
        public string tempPassword { get; set; }
    }
}
