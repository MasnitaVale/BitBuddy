using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Entities
{
    public class Report
    {
        public int id { get; set; }
        public string ReasonDescription { get; set; }
        public ApplicationUser User { get; set; }

        // anonymous reports
    }
}
