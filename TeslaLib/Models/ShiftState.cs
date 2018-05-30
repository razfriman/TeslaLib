using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum ShiftState
    {
        [EnumMember(Value = "D")]
        Drive,

        [EnumMember(Value = "N")]
        Neutral,

        [EnumMember(Value = "P")]
        Park,

        [EnumMember(Value = "R")]
        Reverse
    }
}
