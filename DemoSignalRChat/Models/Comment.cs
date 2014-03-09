using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        public DateTime TimeComment { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }

        public string StatusId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<LikeComment> LikeComments { get; set; }
    }
}