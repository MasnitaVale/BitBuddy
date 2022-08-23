using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Entities
{
    public class UserFriend
    {
        public string User1Id { get; set; }
        public ApplicationUser User1 { get; set; }
        public string User2Id { get; set; }
        public ApplicationUser User2 { get; set; }

    }
}
