namespace TuyaLocal.Commands
{
    internal class RemoveDevice
    {
        public RemoveDevice(string deviceId) =>
            DeviceId = deviceId;

        public string DeviceId { get; }
    }
}
