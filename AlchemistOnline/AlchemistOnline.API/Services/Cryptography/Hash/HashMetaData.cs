using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Services.Cryptography.Hash
{
    public struct HashMetaData
    {
        public int HashingIterations { get; set; }
        public int HashLength { get; set; }

        public HashMetaData(byte[] bytes)
        {
            HashingIterations = 0;
            HashLength = 0;

            int size = Marshal.SizeOf(this);
            IntPtr pointer = Marshal.AllocHGlobal(size);

            Marshal.Copy(bytes, 0, pointer, size);
            this = (HashMetaData)Marshal.PtrToStructure(pointer, GetType());
            Marshal.FreeHGlobal(pointer);
        }

        public byte[] GetBytes()
        {
            int size = Marshal.SizeOf(this);
            byte[] bytes = new byte[size];

            IntPtr pointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(this, pointer, true);
            Marshal.Copy(pointer, bytes, 0, size);
            Marshal.FreeHGlobal(pointer);

            return bytes;
        }
    }
}
