namespace TuyaLocal.Commands
{
    using Models;

    public class AddDevice
    {
        public AddDevice(Device device) => 
            Device = device;

        public Device Device { get; }
    }
}
