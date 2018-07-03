namespace TuyaLocal.Core.Network.Models.Base
{
    using System.Collections.Generic;

    public interface ITuyaBaseResponse
    {
        TuyaBaseResponse Deserialize(IEnumerable<byte> payload);
    }
}
