using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum PerformanceConfiguration
    {
        [EnumMember(Value = "Base")]
        Base,

        [EnumMember(Value = "Sport")]
        Sport,

        [EnumMember(Value = "P2")]
        P2
    }
}
