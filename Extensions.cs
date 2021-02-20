using System;
using System.IO;

namespace UnturnedDataParser
{
    /// <summary>
    /// Stream extension methods to adapt Unturned's Block class operations
    /// </summary>
    public static class Extensions
    {
        public static ushort ReadUInt16(this Stream stream)
        {
            byte[] buffer = new byte[2];
            int r = stream.Read(buffer, 0, 2);
            if (r != 2) throw new EndOfStreamException();
            return BitConverter.ToUInt16(buffer, 0);
        }

        public static byte[] ReadBlockBuffer(this Stream stream)
        {
            int len = stream.ReadByte();
            if (len == -1) throw new EndOfStreamException();
            byte[] buffer = new byte[len];
            stream.Read(buffer, 0, len);
            return buffer;
        }

        public static byte ToByte(this bool Bool)
        {
            if (Bool) return 0x1;
            return 0x0;
        }

        public static void WriteBlockBuffer(this Stream stream, byte[] buffer)
        {
            int len = buffer.Length;
            if (len > 255) throw new ArgumentException("Buffer length exceeds 255 bytes");
            stream.WriteByte((byte)len);
            stream.Write(buffer, 0, len);
        }
    }
}