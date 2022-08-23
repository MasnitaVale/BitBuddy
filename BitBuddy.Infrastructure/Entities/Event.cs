using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        public ICollection<EventInterest> EventInterests { get; set; }
        public ICollection<EventUserRegistration> EventUserRegistrations { get; set; }

    }
}
