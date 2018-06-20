namespace TuyaLocal.Commands
{
    using System.ComponentModel.DataAnnotations;

    public class GetDevice
    {
        public GetDevice(string id) => Id = id;

        [Required]
        [StringLength(16, MinimumLength = 10)]
        public string Id { get; }
    }
}
