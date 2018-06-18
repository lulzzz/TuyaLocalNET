namespace TuyaLocal.Commands
{
    public class RemoveDevice
    {
        public RemoveDevice(string id) =>
            Id = id;

        public string Id { get; }
    }
}
