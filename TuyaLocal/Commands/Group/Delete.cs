namespace TuyaLocal.Commands.Group
{
    using System.ComponentModel.DataAnnotations;

    public class Delete
    {
        public Delete(string groupName) => GroupName = groupName;

        [Required]
        [StringLength(16, MinimumLength = 1)]
        public string GroupName { get; }
    }
}
