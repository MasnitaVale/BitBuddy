using BitBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Dtos
{
    public class AppUserSuggestionDto : ApplicationUser
    {
        public bool IsReportBoxVisible { get; set; } = false;
    }
}
