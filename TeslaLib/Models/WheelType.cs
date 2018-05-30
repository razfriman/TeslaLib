using System.Runtime.Serialization;

namespace TeslaLib.Models
{
    public enum WheelType
    {
        [EnumMember(Value = "Base19")]
        Base19,

        [EnumMember(Value = "Silver21")]
        Silver21,

        [EnumMember(Value = "Charcoal21")]
        Charcoal21,

        CharcoalPerformance21
    }
}
