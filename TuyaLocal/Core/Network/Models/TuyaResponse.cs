namespace TuyaLocal.Core.Network.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class TuyaResponse
    {
        public readonly List<byte> Suffix =
            new List<byte> {0, 0, 170, 85};

        public byte OpCode;

        public IEnumerable<byte> Payload;

        public List<byte> Prefix =
            new List<byte> {0, 0, 85, 170, 0, 0, 0, 0, 0, 0, 0};

        public int Size;

        public int Unknown;

        public TuyaResponse (IEnumerable<byte> buffer)
        {
            if (buffer == null)
            {
                return;
            }

            using (var m = new MemoryStream(buffer.ToArray()))
            {
                using (var reader = new BinaryReader(m))
                {
                    Prefix = reader.ReadBytes(Prefix.Count).ToList();
                    OpCode = reader.ReadByte();

                    Size = BitConverter.ToInt32(
                        reader.ReadBytes(4).Reverse().ToArray(),
                        0);

                    Unknown = reader.ReadInt32();

                    Payload = reader.ReadBytes(
                        Size - Prefix.Count - 1);

                    while (reader.BaseStream.Position <
                           reader.BaseStream.Length)
                    {
                        Suffix.Append(reader.ReadByte());
                    }
                }
            }
        }
    }
}
