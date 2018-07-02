namespace TuyaLocal.Core.Network.Models
{
    using System.Collections.Generic;

    public interface ITuyaResponse
    {
        TuyaResponse Deserialize(IEnumerable<byte> payload);
    }
}
