using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TuyaLocal.Core.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EDeviceState
    {
        On, //the device is turned on and responding
        Off, //the device is turned off but responding
        NoResponse, //the device has hung up
        NoConnection //some devices unexpectedly close the connection
    }
}