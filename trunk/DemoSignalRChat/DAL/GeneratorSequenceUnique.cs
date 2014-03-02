using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace DemoSignalRChat.DAL
{
    public class GeneratorSequenceUnique
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        public static extern int UuidCreateSequential(out Guid guid);

        const int RPC_S_OK = 0;
        public static Guid CreateGuid()
        {
            Guid guid;
            int result = GeneratorSequenceUnique.UuidCreateSequential(out guid);
            if (result == RPC_S_OK)
                return guid;
            else
                return Guid.NewGuid();
        }
    }
}