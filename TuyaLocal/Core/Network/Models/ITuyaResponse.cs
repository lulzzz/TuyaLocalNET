namespace TuyaLocal.Core.Network.Models
{
    internal interface ITuyaResponse
    {
        CAck Serialize(byte[] buffer);
    }
}