namespace TuyaLocal.Commands.Device
{
    using System.ComponentModel.DataAnnotations;
    using Models;

    public class Remove : IDeviceCommand
    {
        public Remove(string id) =>
            Id = id;

        [Required]
        [StringLength(16, MinimumLength = 10)]
        public string Id { get; }
    }
}
