namespace TuyaLocal.Commands.Group
{
    using System.ComponentModel.DataAnnotations;

    public class RemoveDevice
    {
        public RemoveDevice(string groupName, string deviceId)
        {
            GroupName = groupName;
            DeviceId = deviceId;
        }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string GroupName { get; }

        [Required]
        [StringLength(20, MinimumLength = 20)]
        public string DeviceId { get; }
    }
}
