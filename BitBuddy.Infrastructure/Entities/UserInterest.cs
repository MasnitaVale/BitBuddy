﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Entities
{
    public class UserInterest
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
