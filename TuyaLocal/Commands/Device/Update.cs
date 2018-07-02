namespace TuyaLocal.Commands.Device
{
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using Models;
    using Validations;

    public class Update : IDeviceCommand
    {
        public Update(
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
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; }

        [IpAddress]
        public IPAddress IpAddress { get; }

        [Required]
        [StringLength(20, MinimumLength = 10)]
        public string SecretKey { get; }

        [Required]
        [StringLength(20, MinimumLength = 20)]
        public string Id { get; }
    }
}
