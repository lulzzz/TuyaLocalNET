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
        [StringLength(16, MinimumLength = 1)]
        public string GroupName { get; }

        [Required]
        [StringLength(16, MinimumLength = 10)]
        public string DeviceId { get; }
    }
}
