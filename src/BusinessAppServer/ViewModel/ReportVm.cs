using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAppServer.ViewModel
{
    public class ReportVm
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string status { get; set; }
    }
}
