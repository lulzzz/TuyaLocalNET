namespace TuyaLocal.Models
{
    using System.Collections.Generic;

    public class Group
    {
        public string Name { get; set; }
        public List<Device> Devices { get; set; }
    }
}
