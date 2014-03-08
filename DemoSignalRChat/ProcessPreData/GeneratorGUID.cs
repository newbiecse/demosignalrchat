using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace DemoSignalRChat.ProcessPreData
{
    public class GeneratorGUID
    {
        [DllImport("rpcrt4.dll", SetLastError = true)]
        public static extern int UuidCreateSequential(out Guid guid);

        const int RPC_S_OK = 0;
        public static string CreateGuid()
        {
            Guid guid;
            int result = GeneratorGUID.UuidCreateSequential(out guid);
            if (result == RPC_S_OK)
                return guid.ToString();
            else
                return Guid.NewGuid().ToString();
        }
    }
}