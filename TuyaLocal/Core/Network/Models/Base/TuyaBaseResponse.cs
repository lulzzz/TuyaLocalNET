namespace TuyaLocal.Core.Network.Models.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class TuyaBaseResponse
    {
        private readonly List<byte> _prefix =
            new List<byte> { 0, 0, 85, 170, 0, 0, 0, 0, 0, 0, 0 };

        private readonly byte _opCode;

        private readonly int _unknown;

        public readonly IEnumerable<byte> Payload;

        private readonly List<byte> _suffix =
            new List<byte> { 0, 0, 170, 85 };

        public TuyaBaseResponse (IEnumerable<byte> buffer)
        {
            if (buffer == null)
            {
                return;
            }

            using (var m = new MemoryStream(buffer.ToArray()))
            {
                using (var reader = new BinaryReader(m))
                {
                    _prefix = reader.ReadBytes(_prefix.Count).ToList();
                    _opCode = reader.ReadByte();

                    var size = BitConverter.ToInt32(
                        reader.ReadBytes(4).Reverse().ToArray(),
                        0);

                    _unknown = reader.ReadInt32();

                    Payload = reader.ReadBytes(
                        size - _prefix.Count - 1);

                    while (reader.BaseStream.Position <
                           reader.BaseStream.Length)
                    {
                        _suffix.Append(reader.ReadByte());
                    }
                }
            }
        }
    }
}
