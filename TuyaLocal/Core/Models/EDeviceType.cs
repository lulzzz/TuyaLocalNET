using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TuyaLocal.Core.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EDeviceType
    {
        Plug, //default
        Bulb,
        Strip,
        Custom
    }
}