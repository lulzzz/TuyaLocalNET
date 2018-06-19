namespace TuyaLocal.Commands
{
    using System.ComponentModel.DataAnnotations;

    public class RemoveDevice
    {
        public RemoveDevice(string id) =>
            Id = id;

        [Required]
        [StringLength(16, MinimumLength = 10)]
        public string Id { get; }
    }
}
