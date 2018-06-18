namespace TuyaLocal.Commands
{
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using Validations;

    public class AddDevice
    {
        public AddDevice(
            string id,
            string name,
            string address,
            string secretKey)
        {
            Id = id;
            Name = name;

            IpAddress = IPAddress.TryParse(address, out var ip)
                ? ip
                : IPAddress.Parse("127.0.0.1");

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
