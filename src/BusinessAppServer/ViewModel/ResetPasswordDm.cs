using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
    public class ResetPasswordDm
    {
        public string emailAddressDm { get; set; }
        public string oldPasswordDm { get; set; }
        public string newPasswordDm { get; set; }
    }
}
