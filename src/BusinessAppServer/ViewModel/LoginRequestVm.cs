using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
    public class LoginRequestVm
    {
        public string username { get; set; }
        public string password { get; set; }
        public string tempPassword { get; set; }

    }
}
