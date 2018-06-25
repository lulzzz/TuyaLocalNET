namespace TuyaLocal.Commands.Device
{
    using System.ComponentModel.DataAnnotations;

    public class Remove
    {
        public Remove(string id) =>
            Id = id;

        [Required]
        [StringLength(16, MinimumLength = 10)]
        public string Id { get; }
    }
}
