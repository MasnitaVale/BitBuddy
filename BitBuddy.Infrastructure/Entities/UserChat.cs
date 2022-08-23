using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Entities
{
    public class UserChat
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
