namespace TuyaLocal.Core.Models
{
    using System.Text;
    using Network;
    using Network.Models;
    using Newtonsoft.Json;

    internal class InfoRequest
    {
        public TuyaResponse Response;

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

            Response = TuyaNetwork.SendRequest(device, payload);
        }
    }
}