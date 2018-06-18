namespace TuyaLocal.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Net;

    public class IpAddressAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var ip = (IPAddress) value;

            return Equals(ip, IPAddress.Loopback)
                ? new ValidationResult(ErrorMessage)
                : ValidationResult.Success;
        }
    }
}