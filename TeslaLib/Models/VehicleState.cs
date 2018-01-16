using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum VehicleState
    {
        [EnumMember(Value = "Online")]
        Online,

        [EnumMember(Value = "Asleep")]
        Asleep,

        [EnumMember(Value = "Offline")]
        Offline,

        [EnumMember(Value = "Waking")]
        Waking,

        // I saw this for half an hour while charging at a Supercharger then while driving.  Perhaps the modem was
        // offline, or Tesla's web service was offline?
        [EnumMember(Value = "unknown")]
        Unknown
    }
}
