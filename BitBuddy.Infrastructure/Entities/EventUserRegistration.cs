using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Entities
{
    public class EventUserRegistration
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int Rating { get; set; }

    }
}
