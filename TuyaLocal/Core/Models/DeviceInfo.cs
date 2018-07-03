namespace TuyaLocal.Core.Models
{
    using System;
    using System.Linq;
    using System.Text;
    using Network.Models;
    using Network.Models.Base;
    using Newtonsoft.Json.Linq;

    public class DeviceInfo
    {
        public DeviceInfo Parse(TuyaBaseResponse baseResponse)
        {
            if (baseResponse.Payload == null)
            {
                State = EDeviceState.NoConnection;

                return this;
            }

            dynamic devInfo = JObject.Parse(
                Encoding.UTF8.GetString(baseResponse.Payload.ToArray()));

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

            return this;
        }

        public EDeviceState State { get; set; }

        public EDeviceType Type { get; set; }
    }
}
