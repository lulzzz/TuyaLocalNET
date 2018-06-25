namespace TuyaLocal.Commands.Group
{
    using System.ComponentModel.DataAnnotations;

    public class AddDevice
    {
        public AddDevice(string groupName, string id)
        {
            GroupName = groupName;
            Id = id;
        }

        [Required]
        [StringLength(16, MinimumLength = 1)]
        public string GroupName { get; }

        [Required]
        [StringLength(16, MinimumLength = 10)]
        public string Id { get; }
    }
}