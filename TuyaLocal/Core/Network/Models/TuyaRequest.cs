namespace TuyaLocal.Core.Network.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    [Serializable]
    public class TuyaRequest : ITuyaRequest
    {
        private readonly List<byte> _prefix =
            new List<byte> {0, 0, 85, 170, 0, 0, 0, 0, 0, 0, 0};

        public byte OpCode { private get; set; }
        public int Size { private get; set; }
        public IEnumerable<byte> Payload { private get; set; }

        private readonly List<byte> _suffix =
            new List<byte> { 0, 0, 0, 0, 0, 0, 170, 85 };

        public byte[] Serialize()
        {
            using (var m = new MemoryStream())
            {
                using (var writer = new BinaryWriter(m))
                {
                    writer.Write(_prefix.ToArray());
                    writer.Write(OpCode);

                    writer.Write(
                        BitConverter.GetBytes(Size + _suffix.Count)
                            .Reverse()
                            .ToArray());

                    writer.Write(Payload.ToArray());
                    writer.Write(_suffix.ToArray());
                }

                return m.ToArray();
            }
        }
    }
}
