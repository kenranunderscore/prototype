namespace prototype.GUI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    internal static unsafe class Utf8Helper
    {
        public static string GetUtf8String(byte* str)
        {
            var byteCount = GetByteCount(str);
            return Encoding.UTF8.GetString(str, byteCount);
        }

        public static void Free(byte* ptr)
        {
            Marshal.FreeHGlobal(new IntPtr(ptr));
        }

        private static int GetByteCount(byte* str)
        {
            var i = 0;
            while (str[i] != 0)
            {
                i++;
            }

            return i;
        }
    }
}
