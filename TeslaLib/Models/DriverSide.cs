using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum DriverSide
    {
        [EnumMember(Value = "LH")]
        LeftHandDrive,

        [EnumMember(Value = "RH")]
        RightHandDrive
    }
}
