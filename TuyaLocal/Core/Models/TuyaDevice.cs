namespace TuyaLocal.Core.Models
{
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

            Info = new DeviceInfo(
                new InfoRequest(this).Response);

            return this;
        }
    }
}
