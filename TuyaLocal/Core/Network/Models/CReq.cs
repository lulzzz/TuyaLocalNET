namespace TuyaLocal.Core.Network.Models
{
    using System;
    using System.IO;

    [Serializable]
    public class CReq : ITuyaRequest
    {
        public byte[] Prefix = { 0, 0, 85, 170, 0, 0, 0, 0, 0, 0, 0 };
        public byte OpCode;
        public int Size;
        public byte[] Payload;
        public byte[] Suffix = { 0, 0, 0, 0, 0, 0, 170, 85 };

        public byte[] Serialize()
        {
            using (var m = new MemoryStream())
            {
                using (var writer = new BinaryWriter(m))
                {
                    writer.Write(Prefix);
                    writer.Write(OpCode);
                    writer.Write(Size);
                    writer.Write(Payload);
                    writer.Write(Suffix);
                }
                return m.ToArray();
            }
        }
    }
}
