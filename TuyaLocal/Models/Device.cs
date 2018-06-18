namespace TuyaLocal.Models
{
    using System.Net;

    public class Device
    {
        public string Name { get; set; }

        public IPAddress Address { get; set; }

        public string Id { get; set; }

        public string Key { get; set; }
    }
}