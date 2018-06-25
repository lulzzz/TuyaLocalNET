namespace TuyaLocal.Commands.Device
{
    using System.ComponentModel.DataAnnotations;

    public class Get
    {
        public Get(string id) => Id = id;

        [Required]
        [StringLength(16, MinimumLength = 10)]
        public string Id { get; }
    }
}
