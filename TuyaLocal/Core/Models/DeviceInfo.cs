namespace TuyaLocal.Core.Models
{
    using System;
    using System.Linq;
    using System.Text;
    using Network.Models;
    using Newtonsoft.Json.Linq;

    public class DeviceInfo
    {
        public EDeviceType Type { get; set; }
        public EDeviceState State { get; set; }

        public DeviceInfo(TuyaResponse response)
        {
            if (response.Payload == null)
            {
                State = EDeviceState.NoConnection;
                return;
            }

            dynamic devInfo = JObject.Parse(
                Encoding.UTF8.GetString(response.Payload.ToArray()));

            try
            {
                if ((bool) devInfo.dps["1"])
                {
                    State = EDeviceState.On;
                }
                else
                {
                    State = EDeviceState.Off;
                }
            }
            catch (Exception)
            {
                State = EDeviceState.NoResponse;
            }
        }
    }
}