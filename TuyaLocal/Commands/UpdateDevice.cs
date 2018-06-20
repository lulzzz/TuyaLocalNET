namespace TuyaLocal.Commands
{
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using Validations;

    public class EditDevice
    {
        public EditDevice(
            string id,
            string name,
            string address,
            string secretKey)
        {
            Id = id;
            Name = name;

            IpAddress = IPAddress.TryParse(address, out var ip)
                ? ip
                : IPAddress.Loopback;

            SecretKey = secretKey;
        }

        [Required]
        [StringLength(16, MinimumLength = 10)]
        public string Id { get; }

        [Required]
        public string Name { get; }

        [IpAddress]
        public IPAddress IpAddress { get; }

        [Required]
        [StringLength(16, MinimumLength = 10)]
        public string SecretKey { get; }
    }
}
