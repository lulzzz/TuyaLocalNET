namespace TuyaLocal.Core.Network.Models
{
    internal interface ITuyaRequest
    {
        byte[] Serialize();
    }
}