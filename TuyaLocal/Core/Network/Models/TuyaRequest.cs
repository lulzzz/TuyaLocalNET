namespace TuyaLocal.Core.Network.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    [Serializable]
    public class TuyaRequest : ITuyaRequest
    {
        public byte OpCode { get; set; }
        public IEnumerable<byte> Payload { get; set; }
        public readonly List<byte> Prefix = new List<byte> { 0, 0, 85, 170, 0, 0, 0, 0, 0, 0, 0 };
        public int Size { get; set; }
        public readonly List<byte> Suffix = new List<byte> { 0, 0, 0, 0, 0, 0, 170, 85 };

        public byte[] Serialize()
        {
            using (var m = new MemoryStream())
            {
                using (var writer = new BinaryWriter(m))
                {
                    writer.Write(Prefix.ToArray());
                    writer.Write(OpCode);
                    writer.Write(BitConverter.GetBytes(Size + Suffix.Count).Reverse().ToArray());
                    writer.Write(Payload.ToArray());
                    writer.Write(Suffix.ToArray());
                }

                return m.ToArray();
            }
        }
    }
}
