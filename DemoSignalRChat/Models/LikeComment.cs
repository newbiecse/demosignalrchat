using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.Models
{
    public class LikeComment
    {
        public string CommentId { get; set; }
        public string UserId { get; set; }

        public DateTime TimeLike { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}