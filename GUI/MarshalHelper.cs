namespace prototype.GUI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    internal static class MarshalHelper
    {
        public static unsafe string GetUtf8String(byte* str)
        {
            var byteCount = GetByteCount(str);
            var bytes = new byte[byteCount];
            Marshal.Copy((IntPtr)str, bytes, 0, byteCount);
            return Encoding.UTF8.GetString(bytes);
        }

        private static unsafe int GetByteCount(byte* str)
        {
            int i = 0;
            while (str[i] != 0)
            {
                i++;
            }

            return i;
        }
    }
}
