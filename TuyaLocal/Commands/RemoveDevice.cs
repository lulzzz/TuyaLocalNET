namespace TuyaLocal.Commands
{
    public class RemoveDevice
    {
        public RemoveDevice(string deviceId) =>
            DeviceId = deviceId;

        public string DeviceId { get; }
    }
}
