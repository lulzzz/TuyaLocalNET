namespace TuyaLocal.Core.Models
{
    public enum EDeviceState
    {
        On, //the device is turned on and responding
        Off, //the device is turned off but responding
        NoResponse, //the device has hung up
        NoConnection //some devices unexpectedly close the connection
    }
}