using System;
using Newtonsoft.Json;
using TeslaLib.Models;

namespace TeslaLib.Converters
{
    public class VehicleOptionsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(VehicleOptions);

        /// <inheritdoc />
        /// <summary>
        /// Convert the Option Codes into a VehicleOptions instance
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
        new VehicleOptions(serializer.Deserialize<string>(reader));

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}