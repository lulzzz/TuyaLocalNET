namespace TuyaLocal.Core.Network.Models
{
    using System;
    using System.IO;

    [Serializable]
    public class CAck
    {
        public byte[] Prefix = { 0, 0, 85, 170, 0, 0, 0, 0, 0, 0, 0 };
        public byte OpCode;
        public int Size;
        public int Unknown;
        public byte[] Payload;
        public byte[] Suffix = {0, 0, 170, 85};

        public CAck Serialize(byte[] buffer)
        {
            var serialized = new CAck();
            using (var m = new MemoryStream(buffer))
            {
                using (var reader = new BinaryReader(m))
                {
                    serialized.Prefix = reader.ReadBytes(11);
                    serialized.OpCode = reader.ReadByte();
                    serialized.Size = reader.ReadInt32();
                    serialized.Unknown = reader.ReadInt32();
                    serialized.Payload = reader.ReadBytes(buffer.Length - 4);
                    serialized.Suffix = reader.ReadBytes(4);
                }

                return serialized;
            }
        }
    }
}
