using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoSignalRChat.ViewModels
{
    public static class TypeAction
    {
        public const int POST_STATUS = 0;
        public const int COMMENT = 1;
        public const int LIKE = 2;
        public const int SHARE = 3;
        public const int ADD_FRIEND = 4;
    }
}