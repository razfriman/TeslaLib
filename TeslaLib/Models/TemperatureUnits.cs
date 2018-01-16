using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum TemperatureUnits
    {
        [EnumMember(Value = "F")]
        FAHRENHEIT,

        [EnumMember(Value = "C")]
        CELSIUS,
    }
}
