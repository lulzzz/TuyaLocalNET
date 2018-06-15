namespace TuyaLocal.Commands
{
    using Models;

    internal class AddDevice
    {
        public AddDevice(Device device) => 
            Device = device;

        public Device Device { get; }
    }
}
