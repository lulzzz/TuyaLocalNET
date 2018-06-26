namespace TuyaLocal.Commands.Group
{
    public class Get
    {
        public Get(string groupName)
        {
            GroupName = groupName;
        }

        public string GroupName { get; }
    }
}
