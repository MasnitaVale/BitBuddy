using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Entities
{
    public class EventInterest
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
