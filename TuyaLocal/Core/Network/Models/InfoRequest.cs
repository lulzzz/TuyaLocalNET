namespace TuyaLocal.Core.Network.Models
{
    using System.Text;
    using Base;
    using Network;
    using Newtonsoft.Json;

    internal class InfoRequest
    {
        public readonly TuyaBaseResponse BaseResponse;

        public InfoRequest(TuyaDevice device)
        {
            var payload = Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(
                    new
                    {
                        gwId = device.Id,
                        devId = device.Id
                    }
                ));

            BaseResponse = TuyaNetwork.SendRequest(device, payload);
        }
    }
}
