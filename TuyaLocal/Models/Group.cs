namespace TuyaLocal.Models
{
    using System.Collections.Generic;

    public class Group
    {
        public string Name { get; set; }
        //maybe possible logic fault?
        public List<string> Devices { get; set; } = new List<string>();
    }
}
