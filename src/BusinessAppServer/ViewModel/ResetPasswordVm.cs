using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
    public class ResetPasswordVm
    {
        public string emailAddressVm { get; set; }
        public string oldPasswordVm{ get; set; }
        public string newPasswordVm { get; set; }
    }
}
