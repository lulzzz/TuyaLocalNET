namespace TuyaLocal.Models
{
    using System.Net;

    public class Device
    {
        public string DeviceName { get; set; }

        public IPAddress DeviceAddress { get; set; }

        public string DeviceId { get; set; }

        public string ProductKey { get; set; }
    }
}