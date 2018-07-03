namespace TuyaLocal.Core.Network.Models
{
    using Core.Models;
    using TuyaLocal.Models;

    public class TuyaDevice : Device
    {
        public int Port { get; set; } = 6668;
        private DeviceInfo Info { get; set; }

        public TuyaDevice GetInfo()
        {
            if (IpAddress == null || Id == null)
            {
                return this;
            }

            Info = new DeviceInfo().Parse(
                new InfoRequest(this).BaseResponse);

            return this;
        }
    }
}
