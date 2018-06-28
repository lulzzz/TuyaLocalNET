namespace TuyaLocal.Core
{
    using Models;

    public class TuyaDevice
    {
        private readonly Device _device;

        public TuyaDevice(
            Device device,
            int port = 6668,
            string version = "3.1")
        {
            _device = device;
            Port = port;
            Version = version;
        }

        private int Port { get; }
        private string Version { get; }
    }
}
