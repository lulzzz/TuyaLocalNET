namespace TuyaLocal.Commands
{
    using System.Collections.Generic;
    using Models;

    public class ListData
    {
        public ListData(IEnumerable<Device> devices) => Devices = devices;

        public IEnumerable<Device> Devices { get; }
    }
}
