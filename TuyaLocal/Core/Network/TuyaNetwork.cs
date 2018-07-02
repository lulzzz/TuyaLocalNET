using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using TuyaLocal.Core.Models;
using TuyaLocal.Core.Network.Models;

namespace TuyaLocal.Core.Network
{
    public static class TuyaNetwork
    {
        public static async Task<IEnumerable<byte>> Send(IEnumerable<byte> data, string ip, int port)
        {
            var tries = 3;
            Exception lastException = null;
            while (tries-- > 0)
            {
                try
                {
                    using (var client = new TcpClient(ip, port))
                    using (var stream = client.GetStream())
                    using (var ms = new MemoryStream())
                    {
                        var buffer = new byte[1024];
                        await stream.WriteAsync(data.ToArray(), 0, data.Count());
                        var bytes = await stream.ReadAsync(buffer, 0, buffer.Length);
                        stream.Close();
                        ms.Write(buffer, 0, bytes);
                        return ms.ToArray();
                    }
                }
                catch (IOException ex)
                {
                    // sockets sometimes drop the connection unexpectedly, so let's 
                    // retry at least once
                    lastException = ex;
                }

                await Task.Delay(1000);
            }

            if (lastException != null)
            {
                throw lastException;
            }

            return null;
        }

        public static TuyaResponse SendRequest(TuyaDevice device, IReadOnlyCollection<byte> payload)
        {
            return new TuyaResponse(Send(new TuyaRequest
            {
                OpCode = 10,
                Payload = payload,
                Size = payload.Count
            }.Serialize(), device.IpAddress.ToString(), device.Port).Result);
        }
    }
}