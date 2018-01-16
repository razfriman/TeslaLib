using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum DriverSide
    {
        [EnumMember(Value = "LH")]
        LEFT_HAND_DRIVE,

        [EnumMember(Value = "RH")]
        RIGHT_HAND_DRIVE,
    }
}
