namespace TuyaLocal.Commands.Device
{
    using System.ComponentModel.DataAnnotations;
    using Models;

    public class Remove : IDeviceCommand
    {
        public Remove(string id) =>
            Id = id;

        [Required]
        [StringLength(20, MinimumLength = 20)]
        public string Id { get; }
    }
}
