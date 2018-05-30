using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum TemperatureUnits
    {
        [EnumMember(Value = "F")]
        Fahrenheit,

        [EnumMember(Value = "C")]
        Celsius
    }
}
