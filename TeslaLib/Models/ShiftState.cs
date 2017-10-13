using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum ShiftState
    {
        [EnumMember(Value = "D")]
        DRIVE,

        [EnumMember(Value = "N")]
        NEUTRAL,

        [EnumMember(Value = "P")]
        PARK,

        [EnumMember(Value = "R")]
        REVERSE,
    }
}
