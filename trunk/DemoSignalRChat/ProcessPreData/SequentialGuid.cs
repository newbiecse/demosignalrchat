﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web;

namespace DemoSignalRChat.ProcessPreData
{
    public static partial class SequentialGuid
    {
            #region Static Fields
            private static readonly RNGCryptoServiceProvider RandomGenerator = new RNGCryptoServiceProvider();
            #endregion

            public static string Create()
            {
                // We start with 16 bytes of cryptographically strong random data.
                byte[] randomBytes = new byte[10];
                SequentialGuid.RandomGenerator.GetBytes(randomBytes);

                long timestamp = DateTime.UtcNow.Ticks / 10000L;

                // Then get the bytes
                byte[] timestampBytes = BitConverter.GetBytes(timestamp);

                // Since we're converting from an Int64, we have to reverse on
                // little-endian systems.
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(timestampBytes);
                }

                byte[] guidBytes = new byte[16];

                // For string and byte-array version, we copy the timestamp first, followed
                // by the random data.
                Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                // If formatting as a string, we have to compensate for the fact
                // that .NET regards the Data1 and Data2 block as an Int32 and an Int16,
                // respectively.  That means that it switches the order on little-endian
                // systems.  So again, we have to reverse.
                Array.Reverse(guidBytes, 0, 4);
                Array.Reverse(guidBytes, 4, 2);

                return new Guid(guidBytes).ToString();
        }
    }
}