using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum ChargingState
    {
        [EnumMember(Value = "Complete")]
        Complete,

        [EnumMember(Value = "Charging")]
        Charging,

        [EnumMember(Value = "Disconnected")]
        Disconnected,

        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "NotCharging")]
        NotCharging,

        [EnumMember(Value = "Starting")]
        Starting,

        [EnumMember(Value = "Stopped")]
        Stopped,

        // As of September 2017, we started seeing this.
        [EnumMember(Value = "NoPower")]
        NoPower
    }
}
