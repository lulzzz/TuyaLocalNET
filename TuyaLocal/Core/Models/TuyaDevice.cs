namespace TuyaLocal.Core.Models
{
    using System.Text;
    using Network;
    using Network.Models;
    using Newtonsoft.Json;
    using TuyaLocal.Models;

    public class TuyaDevice : Device
    {
        public int Port { get; set; } = 6668;
        public DeviceInfo Info { get; set; }

        public TuyaDevice GetInfo()
        {
            if (IpAddress == null || Id == null)
            {
                return null;
            }

            var payload = Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(
                    new
                    {
                        gwId = Id,
                        devId = Id
                    }
                ));

            var request = new TuyaRequest
            {
                OpCode = 10,
                Payload = payload,
                Size = payload.Length
            }.Serialize();

            var result = TuyaNetwork.Send(request, IpAddress.ToString(), Port)
                .Result;

            Info = new DeviceInfo(
                new TuyaResponse(result));


            return this;
        }

        public void TurnOn()
        {
            //TuyaNetwork.Send(new TurnOnRequest());
            if (Info != null)
            {
                Info.State = EDeviceState.On;
            }
        }

        public void TurnOff()
        {
            //TuyaNetwork.Send(new TurnOffRequest());
            if (Info != null)
            {
                Info.State = EDeviceState.Off;
            }
        }
    }
}