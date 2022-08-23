using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string? Text { get; set; }
        public DateTime CreationDate { get; set; }
        public string? PicturePath { get; set; }
    }
}
