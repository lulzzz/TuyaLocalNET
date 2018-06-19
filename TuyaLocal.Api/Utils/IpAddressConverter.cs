namespace TuyaLocal.Api.Utils
{
    using System;
    using System.Net;
    using Newtonsoft.Json;

    public class IpAddressConverter
        : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            objectType == typeof(IPAddress);

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer) => writer.WriteValue(value.ToString());

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer) =>
            IPAddress.Parse((string) reader.Value);
    }
}
